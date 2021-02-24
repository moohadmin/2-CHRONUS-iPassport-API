using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
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

        public AccountService(IUserDetailsRepository userDetailsRepository, ITokenService tokenService,
            UserManager<Users> userManager, IUserRepository userRepository, IAuth2FactService auth2FactService)
        {
            _userDetailsRepository = userDetailsRepository;
            _tokenService = tokenService;
            _userManager = userManager;
            _userRepository = userRepository;
            _auth2FactService = auth2FactService;
        }

        public async Task<ResponseApi> BasicLogin(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return new ResponseApi(false, "Usuário não cadastrado!", null);

            var userDetails = await _userDetailsRepository.FindWithUser(user.Id);

            /// Caso exista um user cadastrado, porém não tem o cadastro da userDetails, 
            /// será excluido o user pois não é permitido um user sem userDetails
            if (userDetails == null)
            {
                await _userManager.DeleteAsync(user);
                return new ResponseApi(false, "Usuário não cadastrado!", null);
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var token = _tokenService.GenerateBasic(user, userDetails);

                if (token == null)
                    return new ResponseApi(false, "Usuário ou Senha inválidos!", null);

                userDetails.UpdateLastLogin();
                _userDetailsRepository.Update(userDetails);

                return new ResponseApi(true, "Usuário Autenticado!", token);
            }

            return new ResponseApi(false, "Usuário ou Senha Inválido!", null);
        }

        public async Task<ResponseApi> EmailLogin(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new BusinessException("Usuário não cadastrado!");

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
                var token = _tokenService.GenerateBasic(user, userDetails);

                if (token == null)
                    throw new BusinessException("Usuário ou Senha inválidos!");

                userDetails.UpdateLastLogin();
                _userDetailsRepository.Update(userDetails);

                return new ResponseApi(true, "Usuário Autenticado!", token);
            }

            throw new BusinessException("Usuário ou Senha Inválido!");
        }

        public async Task<ResponseApi> MobileLogin(string pin, int documentType, string document)
        {
            var userDetails = await _userDetailsRepository.FindByDocument(documentType, document);
            if(userDetails == null)
                throw new BusinessException("Usuário não cadastrado!");

            var pinvalid =  _auth2FactService.ValidPin(userDetails.UserId, pin);

            var user = _userRepository.FindById(userDetails.UserId);

            var token = _tokenService.GenerateBasic(user.Result, userDetails);

            if(token != null)
            {
                userDetails.UpdateLastLogin();
                _userDetailsRepository.Update(userDetails);

                return new ResponseApi(true, "Usuário Autenticado!", token);
            }

            return new ResponseApi(false, "Ops, ocorreu um erro no login, tente novamente!", null);
        }

        public ResponseApi SendPin(string phone, string doctype, string doc)
        {
            var user = _userRepository.FindByPhone(phone).Result;
            //var userDetails = _userDetailsRepository.FindWithUser((user.Id);

            if (user == null)
                throw new BusinessException("Usuário não cadastrado!");

            var pinresp =  _auth2FactService.SendPin(user.Id, phone);

            return new ResponseApi(true, "PIN Enviado com sucesso!", pinresp);
        }
    }
}