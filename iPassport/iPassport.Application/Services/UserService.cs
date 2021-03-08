using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDetailsRepository _detailsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly UserManager<Users> _userManager;
        private readonly IStorageExternalService _storageExternalService;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IVaccineRepository _vaccineRepository;

        public UserService(IUserRepository userRepository, IUserDetailsRepository detailsRepository, IPlanRepository planRepository, IMapper mapper, IHttpContextAccessor accessor, UserManager<Users> userManager,
            IStorageExternalService storageExternalService, IStringLocalizer<Resource> localizer, ICompanyRepository companyRepository, ICityRepository cityRepository, IVaccineRepository vaccineRepository)
        {
            _userRepository = userRepository;
            _detailsRepository = detailsRepository;
            _planRepository = planRepository;
            _mapper = mapper;
            _accessor = accessor;
            _userManager = userManager;
            _storageExternalService = storageExternalService;
            _localizer = localizer;
            _companyRepository = companyRepository;
            _cityRepository = cityRepository;
            _vaccineRepository = vaccineRepository;
        }

        public async Task<ResponseApi> AddCitizen(CitizenCreateDto dto)
        {
            var user = new Users().CreateCitizen(dto);
            user.SetUpdateDate();
            
            if (dto.CompanyId.HasValue)
            {
                var company = await _companyRepository.Find(dto.CompanyId.Value);
                if (company == null)
                    throw new BusinessException(_localizer["CompanyNotFound"]);
            }

            var city = await _cityRepository.Find(dto.Address.CityId);
            if (city == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            foreach (var d in dto.Doses)
            {
                var vaccine = await _vaccineRepository.Find(d.VaccineId);
                if (vaccine == null)
                    throw new BusinessException(_localizer["VaccineNotFound"]);
            }

            try
            {
                /// Add User in iPassportIdentityContext
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                    throw new BusinessException(_localizer["UserNotCreated"]);

                /// Re-Hidrated UserId to UserDetails
                dto.Id = user.Id;

                /// Add Details to User in iPassportContext
                var userDetails = new UserDetails().Create(dto);

                await _detailsRepository.InsertAsync(userDetails);

                return new ResponseApi(result.Succeeded, _localizer["UserCreated"], user.Id);
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("IX_Users_CNS"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CNS"));
                if (ex.ToString().Contains("IX_Users_CPF"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CPF"));
                if (ex.ToString().Contains("IX_Users_RG"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "RG"));
                if (ex.ToString().Contains("IX_Users_InternationalDocument"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "InternationalDocument"));
                if (ex.ToString().Contains("IX_Users_PassportDoc"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "PassportDoc"));
                if (ex.ToString().Contains("IX_Users_Email"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "E-mail"));
                if (ex.ToString().Contains("IX_Users_PhoneNumber"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "Phone"));

                throw;
            }
        }

        public async Task<ResponseApi> GetCurrentUser()
        {
            var userId = _accessor.GetCurrentUserId();
            var authUser = await _userManager.FindByIdAsync(userId.ToString());

            if (authUser.IsCitizen())
                authUser.Photo = _storageExternalService.GeneratePreSignedURL(authUser.Photo);

            var userDetailsViewModel = _mapper.Map<UserDetailsViewModel>(authUser);

            return new ResponseApi(true, "User Loged", userDetailsViewModel);
        }

        public async Task<ResponseApi> AssociatePlan(Guid planId)
        {
            var userId = _accessor.GetCurrentUserId();

            var userDetails = await _detailsRepository.GetByUserId(userId);
            var plan = await _planRepository.Find(planId);

            if (plan == null)
                throw new BusinessException(_localizer["PlanNotFound"]);

            userDetails.AssociatePlan(plan.Id);
            userDetails.Plan = plan;

            var result = await _detailsRepository.Update(userDetails);
            if (!result)
                throw new BusinessException(_localizer["OperationNotPerformed"]);

            return new ResponseApi(true, _localizer["PlanAssociated"], _mapper.Map<PlanViewModel>(plan));
        }

        public async Task<ResponseApi> GetUserPlan()
        {
            var userId = _accessor.GetCurrentUserId();

            var userDetails = await _detailsRepository.GetByUserId(userId);

            if (userDetails.Plan == null)
                throw new BusinessException(_localizer["PlanNotFound"]);

            return new ResponseApi(true, "User plan", _mapper.Map<PlanViewModel>(userDetails.Plan));
        }

        public async Task<ResponseApi> AddUserImage(UserImageDto userImageDto)
        {
            userImageDto.UserId = _accessor.GetCurrentUserId();
            var user = await _userManager.FindByIdAsync(userImageDto.UserId.ToString());

            if (user == null)
                throw new BusinessException(_localizer["UserNotFound"]);

            if (user.UserHavePhoto())
                throw new BusinessException("Usuário já Tem Foto Cadastrada");

            user.PhotoNameGenerator(userImageDto);
            var imageUrl = await _storageExternalService.UploadFileAsync(userImageDto.ImageFile, userImageDto.FileName);
            user.AddPhoto(imageUrl);

            await _userManager.UpdateAsync(user);

            return new ResponseApi(true, _localizer["ImageAdded"], user.Photo);
        }

        public async Task<ResponseApi> GetLoggedCitzenCount()
        {
            var res = await _userRepository.GetLoggedCitzenCount();

            return new ResponseApi(true, _localizer["CitzenCount"], res);
        }

        public async Task<ResponseApi> GetRegisteredUserCount(GetRegisteredUserCountFilter filter)
        {
            var res = await _userRepository.GetRegisteredUserCount(filter);

            return new ResponseApi(true, _localizer["UserCount"], res);
        }

        public async Task<ResponseApi> GetLoggedAgentCount()
        {
            var res = await _userRepository.GetLoggedAgentCount();

            return new ResponseApi(true, _localizer["AgentCount"], res);
        }

        public async Task<ResponseApi> AddAgent(UserAgentCreateDto dto)
        {
            dto.Profile = (int)EProfileType.Agent;

            var company = await _companyRepository.Find(dto.CompanyId);
            if (company == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);

            var city = await _cityRepository.Find(dto.Address.CityId);
            if (city == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            var user = new Users().CreateAgent(dto);
            user.SetUpdateDate();

            try
            {
                /// Add User in iPassportIdentityContext
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                    throw new BusinessException(_localizer["UserNotCreated"]);

                //var _role = await _userManager.AddToRoleAsync(user, "chronus:web:admin");
                //if (!_role.Succeeded)
                //{
                //    await _userManager.DeleteAsync(user);
                //    throw new BusinessException(_localizer["UserNotCreated"]);
                //}

                /// Re-Hidrated UserId to UserDetails
                dto.UserId = user.Id;

                /// Add Details to User in iPassportContext
                var _userDetails = new UserDetails();
                var userDetails = _userDetails.Create(dto);
                await _detailsRepository.InsertAsync(userDetails);

                return new ResponseApi(result.Succeeded, _localizer["UserCreated"], user.Id);
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("IX_Users_CNS"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CNS"));
                if (ex.ToString().Contains("IX_Users_CPF"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CPF"));
                if (ex.ToString().Contains("IX_Users_RG"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "RG"));
                if (ex.ToString().Contains("IX_Users_InternationalDocument"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "InternationalDocument"));
                if (ex.ToString().Contains("IX_Users_PassportDoc"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "PassportDoc"));
                if (ex.ToString().Contains("IX_Users_Email"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "E-mail"));
                if (ex.ToString().Contains("IX_Users_PhoneNumber"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "Phone"));

                throw;
            }
        }

        public async Task<PagedResponseApi> FindCitizensByNameParts(GetByNamePartsPagedFilter filter)
        {
            var res = await _userRepository.FindCitizensByNameParts(filter);

            var result = _mapper.Map<IList<CitizenByNameViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["Citizens"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);

        }
    }
}
