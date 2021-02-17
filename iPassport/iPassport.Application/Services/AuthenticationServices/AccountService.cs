using System.Threading.Tasks;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;

namespace iPassport.Application.Services.AuthenticationServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<ResponseApi> BasicLogin(string username, string password, string document)
        {
            var docValid = await _userRepository.FindWithDoc(document);
            if (docValid == null)
                return new ResponseApi(false, "Usuário não cadastrado!", null);

            var user = await _accountRepository.BasicLogin(username, password);
            if(user == null)
                return new ResponseApi(false, "Usuário ou Senha inválidos!", null);

            var token = _tokenService.GenerateBasic(user);

            return new ResponseApi(true, "Usuário Autenticado!", token);
        }


    }
}