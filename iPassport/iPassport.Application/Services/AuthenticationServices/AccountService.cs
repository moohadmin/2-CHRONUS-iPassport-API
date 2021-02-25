using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
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

        public async Task<ResponseApi> MobileLogin(int pin, Guid userId)
        {
            var userDetails = await _userDetailsRepository.FindWithUser(userId);

            if(userDetails == null)
                throw new BusinessException("Usuário não cadastrado!");

            var pinvalid =  await _auth2FactService.ValidPin(userDetails.UserId, pin.ToString("0000"));

            var user = await _userRepository.FindById(userDetails.UserId);

            var token = _tokenService.GenerateBasic(user, userDetails);

            if(token != null)
            {
                userDetails.UpdateLastLogin();
                _userDetailsRepository.Update(userDetails);

                return new ResponseApi(true, "Usuário Autenticado!", token);
            }

            throw new BusinessException("Ops, ocorreu um erro no login, tente novamente!");
        }

        public async Task<ResponseApi> SendPin(string phone, EDocumentType doctype, string doc)
        {
            var userDetails = await _userDetailsRepository.FindByDocument(doctype, doc);            
            if(userDetails == null)
                throw new BusinessException("Usuário não cadastrado!");

            var user = await  _userRepository.FindById(userDetails.UserId);
            if (user == null)
                throw new BusinessException("Usuário não cadastrado!");

            if (!String.IsNullOrWhiteSpace(user.PhoneNumber) && user.PhoneNumber != phone)
                throw new BusinessException("Usuário com dados de acesso invalidos!");


            var pinresp =  await _auth2FactService.SendPin(user.Id, phone);


            var AmbienteSimulado = Environment.GetEnvironmentVariable("PIN_INTEGRATION_SIMULADO");
            if (!string.IsNullOrWhiteSpace(AmbienteSimulado) && Convert.ToBoolean(AmbienteSimulado))
                return new ResponseApi(true, "PIN Enviado Em ambiente simulado! Objeto completo no modo não simulado somente retorna o UserId", pinresp);

            return new ResponseApi(true, "PIN Enviado com sucesso!", pinresp.UserId);
        }
    }
}