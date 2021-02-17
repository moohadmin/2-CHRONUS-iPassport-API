using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDetailsRepository _detailsRepository;

        public UserService(IUserRepository userRepository, IUserDetailsRepository detailsRepository)
        {
            _userRepository = userRepository;
            _detailsRepository = detailsRepository;
        }

        public async Task<ResponseApi> Add(UserCreateDto dto)
        {
            var user = new User();
            var userCreated = user.Create(dto);

            dto.UserId = userCreated.Id;
            var userDetails = new UserDetails();
            var userDetailsCreated = userDetails.Create(dto);

            await _userRepository.InsertAsync(userCreated);
            await _detailsRepository.InsertAsync(userDetailsCreated);

            return new ResponseApi(true, "Usuário criado com sucesso!", user);
        }
    }
}
