using System.Threading.Tasks;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Repositories;

namespace iPassport.Application.Services.AuthenticationServices
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public async Task<ResponseApi> BasicLogin(string username, string password)
        {
            var user = await _repository.BasicLogin(username, password);
            if(user == null)
                return new ResponseApi(false, "Usuário não encontrado...", null);

            var token = _tokenService.Generate(user);

            var resp = new BasicLoginViewModel()
            {
                User = user.Username,
                Profile = user.Profile,
                Token = token
            };

            return new ResponseApi(true, "iPassport Api is Healthy!", resp);
        }
    }
}