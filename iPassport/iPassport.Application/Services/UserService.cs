using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDetailsRepository _detailsRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _accessor;

        private readonly UserManager<Users> _userManager;

        public UserService(IUserDetailsRepository detailsRepository, IUserRepository userRepository, IPlanRepository planRepository, IHttpContextAccessor accessor, UserManager<Users> userManager)
        {
            _detailsRepository = detailsRepository;
            _userRepository = userRepository;
            _planRepository = planRepository;
            _accessor = accessor;
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

        public async Task<ResponseApi> AssociatePlan(Guid planId)
        {
            var user = await GetCurrentUser();
            
            user.AssociatePlan(planId);
            _userRepository.Update(user);

            return new ResponseApi(true, "Plano associado com sucesso", user);
        }

        public async Task<ResponseApi> GetUserPlan()
        {
            var user = await GetCurrentUser();
            var plan = await _planRepository.Find(user.Id);

            return new ResponseApi(true, "Plano do usuário", plan);
        }

        private async Task<User> GetCurrentUser()
        {
            var userId = _accessor.HttpContext.User.GetLoggedUserId<Guid>();
            var user = await _userRepository.Find(userId);
           
            if (user == null)
                throw new NotFoundException("Usuário não encontrado");

            return user;
        }
    }
}
