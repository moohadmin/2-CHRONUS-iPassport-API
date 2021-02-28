using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDetailsRepository _detailsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly UserManager<Users> _userManager;
        private readonly IExternalStorageService _externalStorageService;
        //private readonly IStorageExternalService _storageExternalService;

        public UserService(IUserRepository userRepository, IUserDetailsRepository detailsRepository, IPlanRepository planRepository, IMapper mapper, IHttpContextAccessor accessor, UserManager<Users> userManager, IExternalStorageService externalStorageService)
        {
            _userRepository = userRepository;
            _detailsRepository = detailsRepository;
            _planRepository = planRepository;
            _mapper = mapper;
            _accessor = accessor;
            _userManager = userManager;
            _externalStorageService = externalStorageService;
            //_storageExternalService = storageExternalService;
        }

        public async Task<ResponseApi> Add(UserCreateDto dto)
        {
            var user = new Users().Create(dto);
            user.SetUpdateDate();

            try
            {
                /// Add User in iPassportIdentityContext
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                    throw new BusinessException("Usuário não pode ser criado!");

                var _role = await _userManager.AddToRoleAsync(user, "chronus:web:admin");
                if (!_role.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    throw new BusinessException("Usuário não pode ser criado!");
                }

                /// Re-Hidrated UserId to UserDetails
                dto.UserId = user.Id;

                /// Add Details to User in iPassportContext
                var _userDetails = new UserDetails();
                var userDetails = _userDetails.Create(dto);
                await _detailsRepository.InsertAsync(userDetails);

                return new ResponseApi(result.Succeeded, "Usuário criado com sucesso!", user.Id);
            }
            catch (Exception ex)
            {
                if(ex.ToString().Contains("IX_Users_CNS"))
                    throw new BusinessException("CNS já cadastrado!");
                if (ex.ToString().Contains("IX_Users_CPF"))
                    throw new BusinessException("CPF já cadastrado!");
                if (ex.ToString().Contains("IX_Users_RG"))
                    throw new BusinessException("RG já cadastrado!");
                if (ex.ToString().Contains("IX_Users_InternationalDocument"))
                    throw new BusinessException("InternationalDocument já cadastrado!");
                if (ex.ToString().Contains("IX_Users_PassportDoc"))
                    throw new BusinessException("Passaporte já cadastrado!");
                if (ex.ToString().Contains("IX_Users_Email"))
                    throw new BusinessException("Email já cadastrado!");
                if (ex.ToString().Contains("IX_Users_PhoneNumber"))
                    throw new BusinessException("Telefone já cadastrado!");

                throw;
            }
        }

        public async Task<ResponseApi> GetCurrentUser()
        {
            var userId = _accessor.GetCurrentUserId();
            var authUser = await _userManager.FindByIdAsync(userId.ToString());
            
            var userDetailsViewModel = _mapper.Map<UserDetailsViewModel>(authUser);

            return new ResponseApi(true, "Usuario Logado", userDetailsViewModel);
        }

        public async Task<ResponseApi> AssociatePlan(Guid planId)
        {
            var userId = _accessor.GetCurrentUserId();

            var userDetails = await _detailsRepository.GetByUserId(userId);
            var plan = await _planRepository.Find(planId);

            if (plan == null)
                throw new BusinessException("Plano não encontrado");

            userDetails.AssociatePlan(plan.Id);
            userDetails.Plan = plan;

            _detailsRepository.Update(userDetails);

            return new ResponseApi(true, "Plano associado com sucesso", _mapper.Map<PlanViewModel>(plan));
        }

        public async Task<ResponseApi> GetUserPlan()
        {
            var userId = _accessor.GetCurrentUserId();

            var userDetails = await _detailsRepository.GetByUserId(userId);

            if (userDetails.Plan == null)
                throw new BusinessException("Plano não encontrado");

            return new ResponseApi(true, "Plano do usuário", _mapper.Map<PlanViewModel>(userDetails.Plan));
        }

        public async Task<ResponseApi> AddUserImage(UserImageDto userImageDto)
        {
            userImageDto.UserId = _accessor.GetCurrentUserId();
            var user = await _userManager.FindByIdAsync(userImageDto.UserId.ToString());

            if (user == null)
                throw new BusinessException("Usuário não cadastrado");

            if (user.UserHavePhoto())
                throw new BusinessException("Usuário já Tem Foto Cadastrada");

            user.PhotoNameGenerator(userImageDto);
            var imageUrl = await _externalStorageService.UploadFileAsync(userImageDto);
            user.AddPhoto(imageUrl);
            
            await _userManager.UpdateAsync(user);

            return new ResponseApi(true, "Imagem Adicionada", user.Photo);
        }

        public async Task<ResponseApi> GetLoggedCitzenCount()
        {
            var res = await _userRepository.GetLoggedCitzenCount();

            return new ResponseApi(true, "Total de cidadãos logados", res);
        }
        
        public async Task<ResponseApi> GetRegisteredUserCount(GetRegisteredUserCountFilter filter)
        {
            var res = await _userRepository.GetRegisteredUserCount(filter);

            return new ResponseApi(true, "Total de Usuários Registrados", res);
        }
    }
}
