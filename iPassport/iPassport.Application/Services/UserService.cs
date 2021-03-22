using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IStorageExternalService _storageExternalService;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IBloodTypeRepository _bloodTypeRepository;
        private readonly IHumanRaceRepository _humanRaceRepository;
        private readonly IPriorityGroupRepository _priorityGroupRepository;
        private readonly IHealthUnitRepository _healthUnitRepository;
        private readonly IUserVaccineRepository _userVaccineRepository;
        private readonly IUserDiseaseTestRepository _userDiseaseTestRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUserDetailsRepository detailsRepository, IPlanRepository planRepository, IMapper mapper, IHttpContextAccessor accessor, UserManager<Users> userManager,
            IStorageExternalService storageExternalService, IStringLocalizer<Resource> localizer, ICompanyRepository companyRepository, ICityRepository cityRepository, IVaccineRepository vaccineRepository,
            IGenderRepository genderRepository, IBloodTypeRepository bloodTypeRepository, IHumanRaceRepository humanRaceRepository, IPriorityGroupRepository priorityGroupRepository, IHealthUnitRepository healthUnitRepository,
            IUserVaccineRepository userVaccineRepository, IUserDiseaseTestRepository userDiseaseTestRepository, IAddressRepository addressRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _detailsRepository = detailsRepository;
            _planRepository = planRepository;
            _mapper = mapper;
            _accessor = accessor;
            _userManager = userManager;
            _storageExternalService = storageExternalService;
            _localizer = localizer;
            _companyRepository = companyRepository;
            _cityRepository = cityRepository;
            _vaccineRepository = vaccineRepository;
            _genderRepository = genderRepository;
            _bloodTypeRepository = bloodTypeRepository;
            _humanRaceRepository = humanRaceRepository;
            _priorityGroupRepository = priorityGroupRepository;
            _healthUnitRepository = healthUnitRepository;
            _userVaccineRepository = userVaccineRepository;
            _userDiseaseTestRepository = userDiseaseTestRepository;
            _addressRepository = addressRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseApi> AddCitizen(CitizenCreateDto dto)
        {
            var user = new Users().CreateCitizen(dto);
            user.SetUpdateDate();

            if (dto.CompanyId.HasValue && await _companyRepository.Find(dto.CompanyId.Value) == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);

            if (dto.GenderId.HasValue && await _genderRepository.Find(dto.GenderId.Value) == null)
                throw new BusinessException(_localizer["GenderNotFound"]);

            if (dto.BloodTypeId.HasValue && await _bloodTypeRepository.Find(dto.BloodTypeId.Value) == null)
                throw new BusinessException(_localizer["BloodTypeNotFound"]);

            if (dto.HumanRaceId.HasValue && await _humanRaceRepository.Find(dto.HumanRaceId.Value) == null)
                throw new BusinessException(_localizer["HumanRaceNotFound"]);

            if (!dto.PriorityGroupId.HasValue || await _priorityGroupRepository.Find(dto.PriorityGroupId.Value) == null)
                throw new BusinessException(_localizer["PriorityGroupNotFound"]);

            if (dto.Address == null || await _cityRepository.Find(dto.Address.CityId) == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            if (dto.Doses != null && dto.Doses.Any())
            {
                if (await _vaccineRepository.Find(dto.Doses.FirstOrDefault().VaccineId) == null)
                    throw new BusinessException(_localizer["VaccineNotFound"]);

                if (!dto.Doses.All(x => x.VaccineId == dto.Doses.FirstOrDefault().VaccineId))
                    throw new BusinessException(_localizer["DifferentVaccinesDoses"]);

                foreach (var item in dto.Doses)
                {
                    if (await _healthUnitRepository.Find(item.HealthUnitId) == null)
                        throw new BusinessException(_localizer["HealthUnitNotFound"]);
                }
            }

            try
            {
                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                /// Add User in iPassportIdentityContext
                var result = await _userManager.CreateAsync(user);
                ValidSaveUserIdentityResult(result);

                /// Re-Hidrated UserId to UserDetails
                dto.Id = user.Id;

                /// Add Details to User in iPassportContext
                var userDetails = new UserDetails().Create(dto);

                await _detailsRepository.InsertAsync(userDetails);
                
                _unitOfWork.CommitIdentity();
                _unitOfWork.CommitPassport();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackIdentity();
                _unitOfWork.RollbackPassport();

                if (ex.ToString().Contains("IX_Users_CNS"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CNS"));
                if (ex.ToString().Contains("IX_Users_CPF"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CPF"));
                if (ex.ToString().Contains("IX_Users_RG"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "RG"));
                if (ex.ToString().Contains("IX_Users_InternationalDocument"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "InternationalDocument"));
                if (ex.ToString().Contains("IX_Users_PassportDoc"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "PassportDoc"));
                if (ex.ToString().Contains("IX_Users_Email"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "E-mail"));
                if (ex.ToString().Contains("IX_Users_PhoneNumber"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "Phone"));

                throw;
            }
            return new ResponseApi(true, _localizer["UserCreated"], user.Id);
        }

        public async Task<ResponseApi> GetCurrentUser()
        {
            var userId = _accessor.GetCurrentUserId();
            var authUser = await _userManager.FindByIdAsync(userId.ToString());

            if (authUser.IsCitizen())
                authUser.Photo = _storageExternalService.GeneratePreSignedURL(authUser.Photo);

            var userDetailsViewModel = _mapper.Map<UserDetailsViewModel>(authUser);

            return new ResponseApi(true, "User Loged", userDetailsViewModel);
        }

        public async Task<ResponseApi> AssociatePlan(Guid planId)
        {
            var userId = _accessor.GetCurrentUserId();

            var userDetails = await _detailsRepository.GetByUserId(userId);
            var plan = await _planRepository.Find(planId);

            if (plan == null)
                throw new BusinessException(_localizer["PlanNotFound"]);

            userDetails.AssociatePlan(plan.Id);
            userDetails.Plan = plan;

            var result = await _detailsRepository.Update(userDetails);
            if (!result)
                throw new BusinessException(_localizer["OperationNotPerformed"]);

            return new ResponseApi(true, _localizer["PlanAssociated"], _mapper.Map<PlanViewModel>(plan));
        }

        public async Task<ResponseApi> GetUserPlan()
        {
            var userId = _accessor.GetCurrentUserId();

            var userDetails = await _detailsRepository.GetByUserId(userId);

            if (userDetails.Plan == null)
                throw new BusinessException(_localizer["PlanNotFound"]);

            return new ResponseApi(true, "User plan", _mapper.Map<PlanViewModel>(userDetails.Plan));
        }

        public async Task<ResponseApi> AddUserImage(UserImageDto userImageDto)
        {
            userImageDto.UserId = _accessor.GetCurrentUserId();
            var user = await _userManager.FindByIdAsync(userImageDto.UserId.ToString());

            if (user == null)
                throw new BusinessException(_localizer["UserNotFound"]);

            if (user.UserHavePhoto())
                throw new BusinessException("Usuário já Tem Foto Cadastrada");

            user.PhotoNameGenerator(userImageDto);
            var imageUrl = await _storageExternalService.UploadFileAsync(userImageDto.ImageFile, userImageDto.FileName);
            user.AddPhoto(imageUrl);

            await _userManager.UpdateAsync(user);

            return new ResponseApi(true, _localizer["ImageAdded"], user.Photo);
        }

        public async Task<ResponseApi> GetLoggedCitzenCount()
        {
            var res = await _userRepository.GetLoggedCitzenCount();

            return new ResponseApi(true, _localizer["CitzenCount"], res);
        }

        public async Task<ResponseApi> GetRegisteredUserCount(GetRegisteredUserCountFilter filter)
        {
            var res = await _userRepository.GetRegisteredUserCount(filter);

            return new ResponseApi(true, _localizer["UserCount"], res);
        }

        public async Task<ResponseApi> GetLoggedAgentCount()
        {
            var res = await _userRepository.GetLoggedAgentCount();

            return new ResponseApi(true, _localizer["AgentCount"], res);
        }

        public async Task<ResponseApi> AddAgent(UserAgentCreateDto dto)
        {
            var company = await _companyRepository.Find(dto.CompanyId);
            if (company == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);

            if (dto.Address != null && await _cityRepository.Find(dto.Address.CityId) == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            var user = new Users().CreateAgent(dto);
            user.SetUpdateDate();

            try
            {
                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                /// Add User in iPassportIdentityContext
                var result = await _userManager.CreateAsync(user, dto.Password);
                ValidSaveUserIdentityResult(result);

                /// Re-Hidrated UserId to UserDetails
                dto.UserId = user.Id;

                /// Add Details to User in iPassportContext
                var _userDetails = new UserDetails();
                var userDetails = _userDetails.Create(dto);
                await _detailsRepository.InsertAsync(userDetails);

                _unitOfWork.CommitIdentity();
                _unitOfWork.CommitPassport();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackIdentity();
                _unitOfWork.RollbackPassport();

                if (ex.ToString().Contains("IX_Users_CNS"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CNS"));
                if (ex.ToString().Contains("IX_Users_CPF"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CPF"));
                if (ex.ToString().Contains("IX_Users_RG"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "RG"));
                if (ex.ToString().Contains("IX_Users_InternationalDocument"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "InternationalDocument"));
                if (ex.ToString().Contains("IX_Users_PassportDoc"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "PassportDoc"));
                if (ex.ToString().Contains("IX_Users_Email"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "E-mail"));
                if (ex.ToString().Contains("IX_Users_PhoneNumber"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "Phone"));

                throw;
            }
            return new ResponseApi(true, _localizer["UserCreated"], user.Id);
        }

        public async Task<PagedResponseApi> GetPaggedCizten(GetCitzenPagedFilter filter)
        {
            var res = await _userRepository.GetPaggedCizten(filter);

            var result = _mapper.Map<IList<CitizenViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["Citizens"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);

        }

        public async Task<ResponseApi> GetCitizenById(Guid id)
        {
            var authUser = await _userRepository.GetLoadedUsersById(id);

            if (authUser == null)
                throw new BusinessException(_localizer["UserNotFound"]);

            var userDetails = await _detailsRepository.GetLoadedUserById(id);

            if (userDetails == null)
                throw new BusinessException(_localizer["UserNotFound"]);

            var citizenDto = new CitizenDetailsDto(authUser, userDetails);
            
            GetHealthUnitAddress(citizenDto.Doses);

            var citizenDetailsViewModel = _mapper.Map<CitizenDetailsViewModel>(citizenDto);

            return new ResponseApi(true, _localizer["Citizen"], citizenDetailsViewModel);
        }

        public async Task<ResponseApi> EditCitizen(CitizenEditDto dto)
        {
            var currentUser = await _userRepository.GetById(dto.Id);
            if (currentUser == null)
                throw new BusinessException(_localizer["CitizenNotFound"]);

            var currentUserDetails = await _detailsRepository.GetLoadedUserById(dto.Id);
            if (currentUserDetails == null)
                throw new BusinessException(_localizer["CitizenNotFound"]);

            await ValidEditCitizen(dto);

            try
            {
                currentUser.ChangeCitizen(dto);
                currentUserDetails.ChangeCitizen(dto);

                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                var result = await _userManager.UpdateAsync(currentUser);
                ValidSaveUserIdentityResult(result);

                if (!(await _detailsRepository.Update(currentUserDetails)))
                    throw new BusinessException(_localizer["UserNotUpdated"]);

                if (dto.Doses == null && currentUserDetails.UserVaccines.Any(x => !x.ExclusionDate.HasValue))
                {
                    var toRemove = currentUserDetails.UserVaccines.Where(x => !x.ExclusionDate.HasValue);
                    foreach (var item in toRemove)
                    {
                        item.Delete();

                        if (!(await _userVaccineRepository.Update(item)))
                            throw new BusinessException(_localizer["UserNotUpdated"]);
                    }
                }
                else if(dto.Doses != null)
                {
                    var toInclude = dto.Doses.Where(x => !currentUserDetails.UserVaccines.Any(y => y.Id == x.Id));
                    var toChange = currentUserDetails.UserVaccines.Where(x => dto.Doses.Any(y => y.Id == x.Id));
                    var toRemove = currentUserDetails.UserVaccines.Where(x => !dto.Doses.Any(y => y.Id == x.Id));

                    foreach (var item in currentUserDetails.UserVaccines.Where(x => !x.ExclusionDate.HasValue))
                    {
                        if (toChange.Any(x => x.Id == item.Id))
                            item.Change(dto.Doses.FirstOrDefault(x => x.Id == item.Id));
                        else if (toRemove.Any(x => x.Id == item.Id))
                            item.Delete();

                        if (!(await _userVaccineRepository.Update(item)))
                            throw new BusinessException(_localizer["UserNotUpdated"]);
                    }

                    foreach (var item in toInclude)
                    {
                        item.UserId = dto.Id;
                        var vaccine = new UserVaccine().Create(item);
                        if (!(await _userVaccineRepository.InsertAsync(vaccine)))
                            throw new BusinessException(_localizer["UserNotUpdated"]);
                    }
                }

                if (dto.Test != null)
                {
                    if (dto.Test.Id.HasValue)
                    {
                        var test = currentUserDetails.UserDiseaseTests.FirstOrDefault(x => x.Id == dto.Test.Id);
                        test.Change(dto.Test);
                        if (!await _userDiseaseTestRepository.Update(test))
                            throw new BusinessException(_localizer["UserNotUpdated"]);
                    }
                    else
                    {
                        dto.Test.UserId = dto.Id;
                        var newTest = new UserDiseaseTest().Create(dto.Test);
                        if (!await _userDiseaseTestRepository.InsertAsync(newTest))
                            throw new BusinessException(_localizer["UserNotUpdated"]);
                    }
                }

                _unitOfWork.CommitIdentity();
                _unitOfWork.CommitPassport();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackIdentity();
                _unitOfWork.RollbackPassport();

                if (ex.ToString().Contains("IX_Users_CNS"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CNS"));
                if (ex.ToString().Contains("IX_Users_CPF"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CPF"));
                if (ex.ToString().Contains("IX_Users_RG"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "RG"));
                if (ex.ToString().Contains("IX_Users_InternationalDocument"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "InternationalDocument"));
                if (ex.ToString().Contains("IX_Users_PassportDoc"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "PassportDoc"));
                if (ex.ToString().Contains("IX_Users_Email"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "E-mail"));
                if (ex.ToString().Contains("IX_Users_PhoneNumber"))
                    throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "Phone"));

                throw;
            }

            return new ResponseApi(true, _localizer["UserUpdated"], currentUser.Id);
        }

        public async Task<bool> ValidEditCitizen(CitizenEditDto dto)
        {
            if (dto.Address == null || await _addressRepository.Find(dto.Address.Id) == null)
                throw new BusinessException(_localizer["AddressNotFound"]);

            if (dto.CompanyId.HasValue && await _companyRepository.Find(dto.CompanyId.Value) == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);

            if (dto.GenderId.HasValue && await _genderRepository.Find(dto.GenderId.Value) == null)
                throw new BusinessException(_localizer["GenderNotFound"]);

            if (dto.BloodTypeId.HasValue && await _bloodTypeRepository.Find(dto.BloodTypeId.Value) == null)
                throw new BusinessException(_localizer["BloodTypeNotFound"]);

            if (dto.HumanRaceId.HasValue && await _humanRaceRepository.Find(dto.HumanRaceId.Value) == null)
                throw new BusinessException(_localizer["HumanRaceNotFound"]);

            if (!dto.PriorityGroupId.HasValue || await _priorityGroupRepository.Find(dto.PriorityGroupId.Value) == null)
                throw new BusinessException(_localizer["PriorityGroupNotFound"]);

            if (await _cityRepository.Find(dto.Address.CityId) == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            if (dto.Doses != null && dto.Doses.Any())
            {
                if (await _vaccineRepository.Find(dto.Doses.FirstOrDefault().VaccineId) == null)
                    throw new BusinessException(_localizer["VaccineNotFound"]);

                if (!dto.Doses.All(x => x.VaccineId == dto.Doses.FirstOrDefault().VaccineId))
                    throw new BusinessException(_localizer["DifferentVaccinesDoses"]);

                foreach (var item in dto.Doses)
                {
                    if (item.Id.HasValue && await _userVaccineRepository.Find(item.Id.Value) == null)
                        throw new BusinessException(_localizer["UserVaccineNotFound"]);

                    if (await _healthUnitRepository.Find(item.HealthUnitId) == null)
                        throw new BusinessException(_localizer["HealthUnitNotFound"]);
                }
            }

            if (dto.Test?.Id != null && await _userDiseaseTestRepository.Find(dto.Test.Id.Value) == null)
                throw new BusinessException(_localizer["UserDiseaseTestsNotFound"]);

            return true;
        }
        private void ValidSaveUserIdentityResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                string err = string.Empty;

                foreach (var error in result.Errors)
                {
                    err += $"{_localizer[error.Code]}\n";
                }

                throw new BusinessException(err);
            }
        }

        private void GetHealthUnitAddress(IList<UserVaccineDetailsDto> userVaccines)
        {
            var loadedUserVaccines = new List<UserVaccineDetailsDto>();

            userVaccines.ToList().ForEach(x =>
            {
                var loadedDoses = new List<VaccineDoseDto>();
                x.Doses.ToList().ForEach(y =>
                {
                    if(y.HealthUnit != null && y.HealthUnit.Address != null)
                    {
                        y.HealthUnit.Address = new AddressDto(_addressRepository.Find(y.HealthUnit.Address.Id.Value).Result);
                    }
                    loadedDoses.Add(y);
                });
                x.Doses = loadedDoses;
                loadedUserVaccines.Add(x);
            });
            userVaccines = loadedUserVaccines;
        }
    }
}
