using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDetailsRepository _detailsRepository;
        private readonly UserManager<Users> _userManager;

        public UserService(IUserDetailsRepository detailsRepository, UserManager<Users> userManager)
        {
            _detailsRepository = detailsRepository;
            _userManager = userManager;
        }

        public async Task<ResponseApi> Add(UserCreateDto dto)
        {
            var user = new Users
            {
                UserName = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.Mobile
            };

            /// Add User in iPassportIdentityContext
            var result = await _userManager.CreateAsync(user, dto.Password);
            if(result.Errors.Equals(0))
                return new ResponseApi(result.Succeeded, "Usuário não pode ser criado!", result.Errors);

            /// Re-Hidrated UserId to UserDetails
            dto.UserId = user.Id;

            /// Add Details to User in iPassportContext
            var userDetails = new UserDetails();
            var userDetailsCreated = userDetails.Create(dto);
            await _detailsRepository.InsertAsync(userDetailsCreated);

            return new ResponseApi(result.Succeeded, "Usuário criado com sucesso!", user.Id);
        }
    }
}
