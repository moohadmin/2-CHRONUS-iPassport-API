using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Services.AuthenticationServices
{
    public class AccountService : IAccountService
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IUserRepository _userRepository;
        
        private readonly UserManager<Users> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IAuth2FactService _auth2FactService;
        private readonly IHttpContextAccessor _acessor;

        public AccountService(IUserDetailsRepository userDetailsRepository, ITokenService tokenService,
            UserManager<Users> userManager, IUserRepository userRepository, IAuth2FactService auth2FactService, IHttpContextAccessor acessor)
        {
            _userDetailsRepository = userDetailsRepository;
            _tokenService = tokenService;
            _userManager = userManager;
            _userRepository = userRepository;
            _auth2FactService = auth2FactService;
            _acessor = acessor;
        }

        public async Task<ResponseApi> BasicLogin(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new BusinessException("Usuário e / ou senha incorreta.Por favor, tente novamente.");

            var userDetails = await _userDetailsRepository.FindWithUser(user.Id);

            /// Caso exista um user cadastrado, porém não tem o cadastro da userDetails, 
            /// será excluido o user pois não é permitido um user sem userDetails
            if (userDetails == null)
            {
                await _userManager.DeleteAsync(user);
                throw new BusinessException("Usuário e / ou senha incorreta.Por favor, tente novamente.");
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var token = _tokenService.GenerateBasic(user, userDetails);

                if (token == null)
                    throw new BusinessException("Usuário e/ou senha incorreta. Por favor, tente novamente.");

                userDetails.UpdateLastLogin();
                _userDetailsRepository.Update(userDetails);

                return new ResponseApi(true, "Usuário Autenticado!", token);
            }
            throw new BusinessException("Usuário e/ou senha incorreta. Por favor, tente novamente.");
        }

        public async Task<ResponseApi> EmailLogin(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new BusinessException("Usuário e/ou senha incorreta. Por favor, tente novamente.");

            var userDetails = await _userDetailsRepository.FindWithUser(user.Id);

            /// Caso exista um user cadastrado, porém não tem o cadastro da userDetails, 
            /// será excluido o user pois não é permitido um user sem userDetails
            if (userDetails == null)
            {
                await _userManager.DeleteAsync(user);
                throw new BusinessException("Usuário não cadastrado!");
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var token = _tokenService.GenerateByEmail(user, userDetails);

                if (token == null)
                    throw new BusinessException("Usuário e/ou senha incorreta. Por favor, tente novamente.");

                userDetails.UpdateLastLogin();
                _userDetailsRepository.Update(userDetails);

                return new ResponseApi(true, "Usuário Autenticado!", token);
            }
            throw new BusinessException("Usuário e/ou senha incorreta. Por favor, tente novamente.");
        }

        public async Task<ResponseApi> MobileLogin(int pin, Guid userId)
        {
            var userDetails = await _userDetailsRepository.FindWithUser(userId);

            if(userDetails == null)
                throw new BusinessException("Usuário e/ou senha incorreta. Por favor, tente novamente.");

            var pinvalid =  await _auth2FactService.ValidPin(userDetails.UserId, pin.ToString("0000"));

            var user = await _userRepository.FindById(userDetails.UserId);

            var token = _tokenService.GenerateBasic(user, userDetails);

            if(token != null)
            {
                userDetails.UpdateLastLogin();
                _userDetailsRepository.Update(userDetails);

                return new ResponseApi(true, "Usuário Autenticado!", token);
            }
            throw new BusinessException("Usuário e/ou senha incorreta. Por favor, tente novamente.");
        }

        public async Task<ResponseApi> SendPin(string phone, EDocumentType doctype, string doc)
        {
            var userDetails = await _userDetailsRepository.FindByDocument(doctype, doc);            
            if(userDetails == null)
                throw new BusinessException("Usuário e/ou senha incorreta. Por favor, tente novamente.");

            var user = await  _userRepository.FindById(userDetails.UserId);
            if (user == null)
                throw new BusinessException("Usuário e/ou senha incorreta. Por favor, tente novamente.");

            if (!String.IsNullOrWhiteSpace(user.PhoneNumber) && user.PhoneNumber != phone)
                throw new BusinessException("Usuário e/ou senha incorreta. Por favor, tente novamente.");

            var pinresp =  await _auth2FactService.SendPin(user.Id, phone);

            return new ResponseApi(true, "PIN Enviado com sucesso!", pinresp.UserId);
        }

        public async Task<ResponseApi> ResetPassword(string password, string passwordConfirm)
        {
            if (password != passwordConfirm)
                throw new BusinessException("As senhas informadas não são iguais. Tente novamente.");

            var user = await _userManager.FindByIdAsync(_acessor.GetCurrentUserId().ToString());
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, passwordConfirm);

            if (result != IdentityResult.Success)
                throw new BusinessException("A senha inserida não se encontra no padrão pré-estabelecido (8 caracteres: deve conter 1 letra, 1 número e 1 caractere especial). Por favor, verifique");

            return new ResponseApi(true, "Senha alterada!", null);
        }

        public async Task<ResponseApi> ResendPin(string phone, Guid userId)
        {
            var pinresp = await _auth2FactService.ResendPin(userId, phone);

            return new ResponseApi(true, "Novo pin enviado", null);
        }
    }
}