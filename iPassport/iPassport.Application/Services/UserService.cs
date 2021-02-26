using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
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
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        private readonly UserManager<Users> _userManager;
        private readonly IExternalStorageService _externalStorageService;

        public UserService(IUserDetailsRepository detailsRepository, IPlanRepository planRepository, IMapper mapper, IHttpContextAccessor accessor, UserManager<Users> userManager, IExternalStorageService externalStorageService)
        {
            _detailsRepository = detailsRepository;
            _planRepository = planRepository;
            _mapper = mapper;
            _accessor = accessor;
            _userManager = userManager;
            _externalStorageService = externalStorageService;
        }

        public async Task<ResponseApi> Add(UserCreateDto dto)
        {
            var user = new Users
            {
                UserName = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.Mobile
            };
            user.SetUpdateDate();
            /// Add User in iPassportIdentityContext
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return new ResponseApi(result.Succeeded, "Usuário não pode ser criado!", result.Errors);

            await _userManager.AddToRoleAsync(user, "chronus:web:admin");

            /// Re-Hidrated UserId to UserDetails
            dto.UserId = user.Id;

            /// Add Details to User in iPassportContext
            var _userDetails = new UserDetails();
            var userDetails = _userDetails.Create(dto);
            await _detailsRepository.InsertAsync(userDetails);

            return new ResponseApi(result.Succeeded, "Usuário criado com sucesso!", user.Id);
        }

        public async Task<ResponseApi> GetCurrentUser()
        {
            var userId = _accessor.GetCurrentUserId();
            var userDetails = await _detailsRepository.FindWithUser(userId);

            return new ResponseApi(true, "Usuario Logado", _mapper.Map<UserDetailsViewModel>(userDetails));
        }

        public async Task<ResponseApi> AssociatePlan(Guid planId)
        {
            var userId = _accessor.GetCurrentUserId();

            var userDetails = await _detailsRepository.FindWithUser(userId);
            var plan = await _planRepository.Find(planId);

            if (plan == null)
                throw new BusinessException("Plano não encontrado");

            userDetails.AssociatePlan(plan.Id);
            userDetails.Plan = plan;

            _detailsRepository.Update(userDetails);

            return new ResponseApi(true, "Plano associado com sucesso", _mapper.Map<UserDetailsViewModel>(userDetails));
        }

        public async Task<ResponseApi> GetUserPlan()
        {
            var userId = _accessor.GetCurrentUserId();

            var userDetails = await _detailsRepository.FindWithUser(userId);
            var plan = await _planRepository.Find((Guid)userDetails.PlanId);

            if (plan == null)
                throw new BusinessException("Plano não encontrado");

            return new ResponseApi(true, "Plano do usuário", _mapper.Map<PlanViewModel>(plan));
        }

        public async Task<ResponseApi> AddUserImage(UserImageDto userImageDto)
        {
            userImageDto.UserId = _accessor.GetCurrentUserId();
            var userDetails = await _detailsRepository.FindWithUser(userImageDto.UserId);

            if (userDetails == null)
                throw new BusinessException("Usuário não cadastrado");

            if (userDetails.UserHavePhoto())
                throw new BusinessException("Usuário já Tem Foto Cadastrada");

            userDetails.PhotoNameGenerator(userImageDto);
            var imageUrl = await _externalStorageService.UploadFileAsync(userImageDto);
            userDetails.AddPhoto(imageUrl);
            _detailsRepository.Update(userDetails);

            return new ResponseApi(true, "Imagem Adicionada", userDetails.Photo);
        }
    }
}
