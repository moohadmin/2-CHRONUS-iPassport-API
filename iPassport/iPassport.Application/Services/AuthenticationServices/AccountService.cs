using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces.Authentication;
using iPassport.Application.Models;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Services.AuthenticationServices
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        private readonly UserManager<Users> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IAuth2FactService _auth2FactService;
        private readonly IHttpContextAccessor _acessor;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IUserDetailsRepository _userDetailsRepository;

        public AccountService(ITokenService tokenService,
            UserManager<Users> userManager, IUserRepository userRepository, IAuth2FactService auth2FactService, IHttpContextAccessor acessor, IStringLocalizer<Resource> localizer, IUserDetailsRepository userDetailsRepository)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _userRepository = userRepository;
            _auth2FactService = auth2FactService;
            _acessor = acessor;
            _localizer = localizer;
            _userDetailsRepository = userDetailsRepository;
        }

        public async Task<ResponseApi> BasicLogin(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new BusinessException(_localizer["UserOrPasswordInvalid"]);

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var token = await _tokenService.GenerateBasic(user, false);

                if (token == null)
                    throw new BusinessException(_localizer["UserOrPasswordInvalid"]);

                user.UpdateLastLogin();
                await _userRepository.Update(user);

                return new ResponseApi(true, _localizer["UserAuthenticated"], token);
            }
            throw new BusinessException(_localizer["UserOrPasswordInvalid"]);
        }

        public async Task<ResponseApi> EmailLogin(string email, string password)
        {
            var user = await ValidateUserToEmailLogin(email);
            var userDetails = await ValidateUserDetailsToEmailLogin(user.Id);
            ValidateProfileDataToToken(user, userDetails);

            var tokenData = GetTokenProfileData(user, userDetails);

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var token = await _tokenService.GenerateByEmail(user, tokenData.CompanyId, tokenData.CityId, tokenData.StateId, tokenData.CountryId, tokenData.HealthUnityId);

                if (token == null)
                    throw new BusinessException(_localizer["UserOrPasswordInvalid"]);

                if (user.LastLogin != null)
                {
                    user.UpdateLastLogin();
                    await _userRepository.Update(user);
                }

                return new ResponseApi(true, _localizer["UserAuthenticated"], token);
            }
            throw new BusinessException(_localizer["UserOrPasswordInvalid"]);
        }

        public async Task<ResponseApi> MobileLogin(int pin, Guid userId, bool acceptTerms)
        {
            if (!acceptTerms)
                throw new BusinessException(_localizer["TermsOfUseNotAccepted"]);

            var user = await _userRepository.GetById(userId);
            if (user == null)
                throw new BusinessException(_localizer["UserNotFound"]);

            await _auth2FactService.ValidPin(user.Id, pin.ToString("0000"));

            var userDetails = await _userDetailsRepository.GetByUserId(userId);
            if (userDetails == null)
                throw new BusinessException(_localizer["UserNotFound"]);
            var hasPlan = userDetails.PlanId.HasValue;

            var token = await _tokenService.GenerateBasic(user, hasPlan);

            if (token != null)
            {
                if (!user.AcceptTerms)
                    user.SetAcceptTerms(acceptTerms);

                user.UpdateLastLogin();
                await _userRepository.Update(user);

                return new ResponseApi(true, _localizer["UserAuthenticated"], token);
            }

            throw new BusinessException(_localizer["UserNotFound"]);
        }

        public async Task<ResponseApi> SendPin(string phone, EDocumentType doctype, string doc)
        {
            var user = await _userRepository.GetByDocument(doctype, doc.Trim());

            if (user == null)
                throw new BusinessException(_localizer["InvalidDocLogin"]);

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && user.PhoneNumber != phone)
                throw new BusinessException(_localizer["UserNotFound"]);

            var pinresp = await _auth2FactService.SendPin(user.Id, phone);

            return new ResponseApi(true, _localizer["PinSent"], pinresp.UserId);
        }

        public async Task<ResponseApi> ResendPin(string phone, Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
                throw new BusinessException(_localizer["UserNotFound"]);

            await _auth2FactService.SendPin(userId, phone);

            return new ResponseApi(true, _localizer["PinSent"], null);
        }

        public async Task<ResponseApi> ResetPassword(string password, string passwordConfirm)
        {
            if (password != passwordConfirm)
                throw new BusinessException(_localizer["PasswordDifference"]);

            var user = await _userManager.FindByIdAsync(_acessor.GetCurrentUserId().ToString());
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, passwordConfirm);

            if (result != IdentityResult.Success)
                throw new BusinessException(_localizer["PasswordOutPattern"]);

            user.UpdateLastLogin();
            await _userRepository.Update(user);

            return new ResponseApi(true, _localizer["PasswordChanged"], null);
        }

        public async Task<ResponseApi> Logout()
        {
            await _tokenService.DeactivateCurrentAsync();

            return new ResponseApi(true, _localizer["Loggedout"]);
        }

        #region Private
        private async Task<Users> ValidateUserToEmailLogin(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
                throw new BusinessException(_localizer["UserOrPasswordInvalid"]);

            if (user.IsInactive())
                throw new BusinessException(_localizer["InactiveUser"]);

            if (user.Profile == null)
                throw new BusinessException(_localizer["UserAccessProfileNotFound"]);

            return user;
        }
        private async Task<UserDetails> ValidateUserDetailsToEmailLogin(Guid userId)
        {
            var userDetails = await _userDetailsRepository.GetByUserId(userId);
            if (userDetails == null)
                throw new BusinessException(_localizer["UserNotFound"]);

            return userDetails;
        }
        private void ValidateProfileDataToToken(Users user, UserDetails UserDetails)
        {
            if(!user.Profile.IsAdmin())
            {
                if (user.Company == null)
                    throw new BusinessException(_localizer["CompanyNotFound"]);

                if (!user.Company.IsActive())
                    throw new BusinessException(_localizer["InactiveUserCompany"]);
                
                if (!user.Profile.IsBusiness())
                {
                    if (user.Company.Address == null)
                        throw new BusinessException(_localizer["AddressNotFound"]);
                    
                    if(user.Profile.IsHealthUnit() && !UserDetails.HealthUnitId.HasValue)
                        throw new BusinessException(_localizer["HealthUnitNotFound"]);
                }
            }
        }
        private TokenProfileDataDto GetTokenProfileData(Users user, UserDetails UserDetails)
        =>
          new()
          {
              CompanyId = GetCompanyId(user),
              CityId = GetCityId(user),
              StateId = GetStateId(user),
              CountryId = GetCountryId(user),
              HealthUnityId = GetHealthUnityId(UserDetails, user.Profile)
          };

        private string GetCompanyId(Users user)
            => user.Profile.IsBusiness() ? user.CompanyId.ToString() : "";
        private string GetCityId(Users user)
            => (user.Profile.IsGovernment() || user.Profile.IsHealthUnit()) ? user.Company.Address.CityId.ToString() : "";
        private string GetStateId(Users user)
            => (user.Profile.IsGovernment() || user.Profile.IsHealthUnit()) ? user.Company.Address.City.StateId.ToString() : "";
        private string GetCountryId(Users user)
            => (user.Profile.IsGovernment() || user.Profile.IsHealthUnit()) ? user.Company.Address.City.State.CountryId.ToString() : "";
        private string GetHealthUnityId(UserDetails UserDetails, Profile profile)
            => profile.IsHealthUnit() ? UserDetails.HealthUnitId.ToString() : "";


        #endregion
    }
}