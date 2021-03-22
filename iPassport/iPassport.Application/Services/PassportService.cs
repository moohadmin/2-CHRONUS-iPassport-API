using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class PassportService : IPassportService
    {
        private readonly IMapper _mapper;
        private readonly IPassportRepository _repository;
        private readonly IPassportDetailsRepository _passportDetailsRepository;
        private readonly IPassportUseRepository _passportUseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IStorageExternalService  _storageExternalService;
        private readonly IStringLocalizer<Resource> _localizer;

        public PassportService(IMapper mapper, IPassportRepository repository, IUserDetailsRepository userDetailsRepository, IPassportUseRepository useRepository, IHttpContextAccessor accessor, IPassportDetailsRepository passportDetailsRepository, IUserRepository userRepository
            , IStorageExternalService storageExternalService, IStringLocalizer<Resource> localizer)
        {
            _mapper = mapper;
            _repository = repository;
            _userDetailsRepository = userDetailsRepository;
            _passportUseRepository = useRepository;
            _accessor = accessor;
            _passportDetailsRepository = passportDetailsRepository;
            _userRepository = userRepository;
            _storageExternalService = storageExternalService;
            _localizer = localizer;
        }

        public async Task<ResponseApi> Get()
        {
            Guid UserId = _accessor.GetCurrentUserId();

            var userDetails = await _userDetailsRepository.GetLoadedUserById(UserId);

            if (userDetails == null)
                throw new BusinessException(_localizer["UserNotFound"]);

            if ((userDetails.UserVaccines == null || !userDetails.UserVaccines.Any(x => x.ExclusionDate == null)) && (userDetails.UserDiseaseTests == null || !userDetails.UserDiseaseTests.Any()))
                throw new BusinessException(_localizer["UserVaccinesNotFound"]);

            var passport = await _repository.FindByUser(userDetails.Id);

            if (passport == null)
            {
                passport = new Passport();
                passport = passport.Create(userDetails);

                await _repository.InsertAsync(passport);
            }
            else
            {
                if (passport.IsAllDetailsExpired())
                {
                    var passportDetails = passport.NewPassportDetails(null);
                    await _passportDetailsRepository.InsertAsync(passportDetails);
                }
            }

            var viewModel = _mapper.Map<PassportViewModel>(passport);
            var authUser = await _userRepository.GetById(UserId);
            
            viewModel.UserFullName = authUser.FullName;
            viewModel.UserPhoto = _storageExternalService.GeneratePreSignedURL(authUser.Photo);
            viewModel.UserPlan = userDetails.Plan?.Type;
            viewModel.PlanColorStart = userDetails.Plan?.ColorStart;
            viewModel.PlanColorEnd = userDetails.Plan?.ColorEnd;

            return new ResponseApi(true, "User Passport", viewModel);
        }

        public async Task<ResponseApi> AddAccessApproved(PassportUseCreateDto dto)
        {
            dto = await ValidPassportToAcess(dto);

            var userDetails = await _userDetailsRepository.GetLoadedUserById(dto.CitizenId);

            if(!userDetails.IsApprovedPassport())
                throw new BusinessException(_localizer["PassportNotApproved"]);
            
            dto.AllowAccess = true;

            var passportUse = new PassportUse();
            passportUse = passportUse.Create(dto);

            var result = await _passportUseRepository.InsertAsync(passportUse);
            
            if (!result)
                throw new BusinessException(_localizer["OperationNotPerformed"]);

            return new ResponseApi(true, _localizer["AprovedAccess"]);
        }

        public async Task<ResponseApi> AddAccessDenied(PassportUseCreateDto dto)
        {
            dto = await ValidPassportToAcess(dto);
            dto.AllowAccess = false;

            var passportUse = new PassportUse();
            passportUse = passportUse.Create(dto);

            var result = await _passportUseRepository.InsertAsync(passportUse);
            
            if (!result)
                throw new BusinessException(_localizer["OperationNotPerformed"]);

            return new ResponseApi(true, _localizer["DeniedAccess"]);
        }

        private async Task<PassportUseCreateDto> ValidPassportToAcess(PassportUseCreateDto dto)
        {
            var passport = await _repository.FindByPassportDetailsValid(dto.PassportDetailsId);

            if (passport == null)
                throw new BusinessException(_localizer["PassportNotFound"]);

            var agentUserDetails = await _userDetailsRepository.GetByUserId(_accessor.GetCurrentUserId());

            if (agentUserDetails == null)
                throw new BusinessException(_localizer["AgentNotFound"]);

            dto.CitizenId = passport.UserDetailsId;
            dto.AgentId = agentUserDetails.Id;

            return dto;
        }

        public async Task<ResponseApi> GetPassportUserToValidate(Guid passportDetailsId)
        {
            var passport = await _repository.FindByPassportDetailsValid(passportDetailsId);

            if (passport == null)
                throw new BusinessException(_localizer["PassportNotFound"]);
            
            var authUser = await _userRepository.GetById(_accessor.GetCurrentUserId());
            if(!authUser.IsAgent())
                throw new BusinessException(_localizer["UserNotAgent"]);
            
            var passportCitizen = await _userRepository.GetById(passport.UserDetails.Id);

            var viewModel = _mapper.Map<PassportToValidateViewModel>(passport);
            viewModel.Cpf = passportCitizen.CPF;
            viewModel.UserPhoto = _storageExternalService.GeneratePreSignedURL(passportCitizen.Photo);
            viewModel.UserFullName = passportCitizen.FullName;
            viewModel.Immunized = passport.UserDetails.IsApprovedPassport();

            return new ResponseApi(true, "Passport para validação",viewModel);
        }
    }
}
