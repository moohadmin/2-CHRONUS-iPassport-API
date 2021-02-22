using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace iPassport.Application.Services.AuthenticationServices
{
    public class AccountService : IAccountService
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly ITokenService _tokenService;
        private readonly UserManager<Users> _userManager;

        public AccountService(IUserDetailsRepository userDetailsRepository, ITokenService tokenService, UserManager<Users> userManager)
        {
            _userDetailsRepository = userDetailsRepository;
            _tokenService = tokenService;
            _userManager = userManager;
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
    }
}