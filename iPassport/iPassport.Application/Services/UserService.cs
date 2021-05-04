using AutoMapper;
using FluentValidation.Results;
using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.CsvMapper;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Dtos.DtoValidator;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;

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
        private readonly IImportedFileRepository _importedFileRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        public UserService(IUserRepository userRepository, IUserDetailsRepository detailsRepository, IPlanRepository planRepository, IMapper mapper, IHttpContextAccessor accessor, UserManager<Users> userManager,
            IStorageExternalService storageExternalService, IStringLocalizer<Resource> localizer, ICompanyRepository companyRepository, ICityRepository cityRepository, IVaccineRepository vaccineRepository,
            IGenderRepository genderRepository, IBloodTypeRepository bloodTypeRepository, IHumanRaceRepository humanRaceRepository, IPriorityGroupRepository priorityGroupRepository, IHealthUnitRepository healthUnitRepository,
            IUserVaccineRepository userVaccineRepository, IUserDiseaseTestRepository userDiseaseTestRepository, IAddressRepository addressRepository, IUnitOfWork unitOfWork, IImportedFileRepository importedFileRepository
            , IProfileRepository profileRepository, IUserTokenRepository userTokenRepository, IUserTypeRepository userTypeRepository)
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
            _importedFileRepository = importedFileRepository;
            _profileRepository = profileRepository;
            _userTokenRepository = userTokenRepository;
            _userTypeRepository = userTypeRepository;
        }

        public async Task<ResponseApi> AddCitizen(CitizenCreateDto dto)
        {
            await ValidateAddCitizen(dto);

            var user = new Users().CreateUser(dto, (await GetUserTypeIdByIdentifierWhenExists(EUserType.Citizen)));

            try
            {
                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                /// Add User in iPassportIdentityContext
                var result = await _userManager.CreateAsync(user);
                ValidateSaveUserIdentityResult(result);

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

                VerifyUniqueKeyErrors(ex);

                throw;
            }
            return new ResponseApi(true, _localizer["UserCreated"], user.Id);
        }

        public async Task<ResponseApi> GetCurrentUser()
        {
            var userId = _accessor.GetCurrentUserId();
            var authUser = await _userRepository.GetById(userId);

            if (authUser.IsCitizen())
                authUser.Photo = _storageExternalService.GeneratePreSignedURL(authUser.Photo);

            var userTypeIdentifyer = _accessor.GetCurrentUserTypeIdentifyer();

            var userDetailsViewModel = _mapper.Map<UserDetailsViewModel>(authUser);
            userDetailsViewModel.LastLogin = !userTypeIdentifyer.HasValue? null : authUser.GetLastLogin(userTypeIdentifyer.Value);

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
        public async Task<PagedResponseApi> GetPagedAgent(GetAgentPagedFilter filter)
        {
            AccessControlDTO accessControl = _accessor.GetAccessControlDTO();

            var res = await _userRepository.GetPagedAgent(filter, accessControl);

            var result = _mapper.Map<IList<AgentViewModel>>(res.Data);           

            return new PagedResponseApi(true, _localizer["Agents"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        public async Task<ResponseApi> AddAgent(UserAgentCreateDto dto)
        {
            var company = await _companyRepository.Find(dto.CompanyId);
            if (company == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);

            if (dto.Address != null && await _cityRepository.Find(dto.Address.CityId) == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            var user = new Users().CreateAgent(dto);


            try
            {
                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                /// Add User in iPassportIdentityContext
                var result = await _userManager.CreateAsync(user, dto.Password);
                ValidateSaveUserIdentityResult(result);

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

                VerifyUniqueKeyErrors(ex);
            }
            return new ResponseApi(true, _localizer["UserCreated"], user.Id);
        }

        public async Task<PagedResponseApi> GetPaggedCizten(GetCitzenPagedFilter filter)
        {
            AccessControlDTO accessControl = await GetCitizenControlData();

            var res = await _userRepository.GetPaggedCizten(filter, accessControl);

            var ImportedInfo = await _detailsRepository.GetImportedUserById(res.Data.Select(x => x.Id).ToArray());

            var result = _mapper.Map<IList<CitizenViewModel>>(res.Data);
            result.ToList().ForEach(x =>
            {
                x.WasImported = (ImportedInfo.FirstOrDefault(y => y.UserId == x.Id)?.WasImported).GetValueOrDefault();
            });

            return new PagedResponseApi(true, _localizer["Citizens"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        public async Task<ResponseApi> GetCitizenById(Guid id)
        {
            var authUser = await _userRepository.GetLoadedCitizenById(id);

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
            AccessControlDTO accessControl = await GetCitizenControlData();

            var currentUser = await _userRepository.GetById(dto.Id);
            
            if (currentUser == null)
                throw new BusinessException(_localizer["CitizenNotFound"]);

            var currentUserDetails = await _detailsRepository.GetLoadedUserById(dto.Id);
            if (currentUserDetails == null)
                throw new BusinessException(_localizer["CitizenNotFound"]);

            await ValidateEditCitizen(dto, accessControl, currentUserDetails, currentUser);

            try
            {
                currentUser.ChangeCitizen(dto);
                currentUserDetails.ChangeCitizen(dto);

                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                var result = await _userManager.UpdateAsync(currentUser);
                ValidateSaveUserIdentityResult(result);

                if (!(await _detailsRepository.Update(currentUserDetails)))
                    throw new BusinessException(_localizer["UserNotUpdated"]);
                
                var isAdmin = accessControl.Profile == EProfileKey.admin.ToString();

                if (dto.Doses == null && currentUserDetails.UserVaccines.Any(x => !x.ExclusionDate.HasValue))
                {
                    if(!isAdmin)
                        throw new BusinessException(_localizer["OnlyAdminCanDeleteVaccineData"]);

                    var toRemove = currentUserDetails.UserVaccines.Where(x => !x.ExclusionDate.HasValue);
                    
                    foreach (var item in toRemove)
                    {
                        item.Delete();

                        if (!(await _userVaccineRepository.Update(item)))
                            throw new BusinessException(_localizer["UserNotUpdated"]);
                    }
                }
                else if (dto.Doses != null)
                {
                    var toInclude = dto.Doses.Where(x => !currentUserDetails.UserVaccines.Any(y => y.Id == x.Id));
                    var toChange = currentUserDetails.UserVaccines.Where(x => dto.Doses.Any(y => y.Id == x.Id));
                    var toRemove = currentUserDetails.UserVaccines.Where(x => !dto.Doses.Any(y => y.Id == x.Id && !x.ExclusionDate.HasValue));

                    if (!isAdmin && toRemove.Any())
                        throw new BusinessException(_localizer["OnlyAdminCanDeleteVaccineData"]);

                    ValidateVaccineDates(toInclude);
                    ValidateVaccineDates(dto.Doses.Where(x => currentUserDetails.UserVaccines.Any(y => y.Id == x.Id)));

                    foreach (var item in currentUserDetails.UserVaccines.Where(x => !x.ExclusionDate.HasValue))
                    {
                        var itemToChange = toChange.FirstOrDefault(x => x.Id == item.Id);
                        var itemChangedDto = dto.Doses.FirstOrDefault(x => x.Id == item.Id);
                        
                        if (itemToChange != null)
                        {
                            if(!itemToChange.CanEditVaccineFields(accessControl, itemChangedDto))
                                throw new BusinessException(_localizer["OnlyAdminCanEditVaccineData"]);

                            item.Change(itemChangedDto);
                        }
                        
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
                    if (dto.Test.IsEmpty())
                        throw new BusinessException(_localizer["TestNotMustBeNullOrEmpty"]);

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
                else if (currentUserDetails.UserDiseaseTests.Any())
                {
                    foreach (var item in currentUserDetails.UserDiseaseTests)
                    {
                        if (!await _userDiseaseTestRepository.Delete(item))
                            throw new BusinessException(_localizer["UserNotUpdated"]);
                    }
                }

                _unitOfWork.CommitIdentity();
                _unitOfWork.CommitPassport();
            }
            catch (BusinessException)
            {
                _unitOfWork.RollbackIdentity();
                _unitOfWork.RollbackPassport();
                
                throw;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackIdentity();
                _unitOfWork.RollbackPassport();

                VerifyUniqueKeyErrors(ex);

                throw;
            }

            return new ResponseApi(true, _localizer["UserUpdated"], currentUser.Id);
        }

        private void ValidateVaccineDates(IEnumerable<UserVaccineAbstractDto> doses)
        {
            var listDoses = doses.OrderBy(x => x.Dose).ToList();

            if (listDoses.Any(x => doses.Any(y => y.Dose != x.Dose && y.VaccinationDate.Date == x.VaccinationDate.Date)))
                throw new BusinessException(_localizer["VaccineDoseDateCannoteBeEquals"]);

            listDoses.ForEach(x =>
            {
                var nextIndex = listDoses.IndexOf(x) <= listDoses.IndexOf(listDoses.Last()) ? listDoses.IndexOf(x) + 1 : listDoses.IndexOf(x);

                if (listDoses.Find(y => listDoses.IndexOf(y) == nextIndex)?.VaccinationDate.Date <= x.VaccinationDate.Date)
                    throw new BusinessException(_localizer["VaccineNextDoseDateCannoteBeLowerToPrevious"]);
            });
        }
        public async Task<ResponseApi> AddAdmin(AdminDto dto)
        {
            await ValidateToSaveAdmin(dto);

            if (!dto.IsActive.GetValueOrDefault())
                dto.DeactivationUserId = _accessor.GetCurrentUserId();
            
           Users user = Users.CreateUser(dto, (await GetUserTypeIdByIdentifierWhenExists(EUserType.Admin)));

            try
            {
                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                /// Add User in iPassportIdentityContext
                var result = await _userManager.CreateAsync(user, dto.Password);
                ValidateSaveUserIdentityResult(result);

                /// Re-Hidrated UserId to UserDetails
                dto.Id = user.Id;

                /// Add Details to User in iPassportContext
                var userDetails = UserDetails.CreateUserDetail(dto);

                await _detailsRepository.InsertAsync(userDetails);

                _unitOfWork.CommitIdentity();
                _unitOfWork.CommitPassport();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackIdentity();
                _unitOfWork.RollbackPassport();

                VerifyUniqueKeyErrors(ex);

                throw;
            }

            return new ResponseApi(true, _localizer["UserCreated"], user.Id);
        }

        public async Task ImportUsers(IFormFile file)
        {
            ValidateFileLenght(file);
            List<CsvMappingResult<UserImportDto>> fileData = ReadCsvData(file);
            ImportedFile importedFile = new(file.FileName, fileData.Count, _accessor.GetCurrentUserId());

            await _importedFileRepository.InsertAsync(importedFile);

            importedFile.ImportedFileDetails = GetExtractionErrors(fileData, importedFile.Id);
            ValidateExtractedData(fileData, importedFile);

            var validData = fileData.Where(f => f.IsValid).ToList();
            await GetComplementaryDatForUserImport(validData, importedFile);
            var citizenUserTypeId = await GetUserTypeIdByIdentifierWhenExists(EUserType.Citizen);
            foreach (var data in validData.Where(d => d.IsValid))
            {
                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                try
                {
                    Users user = Users.CreateUser(data.Result, citizenUserTypeId);

                    // Add User in iPassportIdentityContext
                    var result = await _userManager.CreateAsync(user);
                    ValidateSaveUserIdentityResult(result);

                    // Re-Hidrated UserId to UserDetails
                    data.Result.UserId = user.Id;

                    // Add Details to User in iPassportContext
                    UserDetails UserDetail = UserDetails.CreateUserDetail(data.Result, importedFile.Id);
                    await _detailsRepository.InsertAsync(UserDetail);

                    _unitOfWork.CommitIdentity();
                    _unitOfWork.CommitPassport();
                }
                catch (Exception ex)
                {
                    _unitOfWork.RollbackIdentity();
                    _unitOfWork.RollbackPassport();

                    if (ex.ToString().Contains("IX_Users_CNS"))
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.Cns.ToString()], string.Format(_localizer["DataAlreadyRegistered"], _localizer["ColumnNameImportFileCns"]), data.RowIndex + 1, importedFile.Id));
                    else if (ex.ToString().Contains("IX_Users_CPF"))
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.Cpf.ToString()], string.Format(_localizer["DataAlreadyRegistered"], _localizer["ColumnNameImportFileCpf"]), data.RowIndex + 1, importedFile.Id));
                    else if (ex.ToString().Contains("IX_Users_Email"))
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.Email.ToString()], string.Format(_localizer["DataAlreadyRegistered"], _localizer["ColumnNameImportFileEmail"]), data.RowIndex + 1, importedFile.Id));
                    else if (ex.ToString().Contains("IX_Users_PhoneNumber"))
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.PhoneNumber.ToString()], string.Format(_localizer["DataAlreadyRegistered"], _localizer["ColumnNameImportFilePhoneNumber"]), data.RowIndex + 1, importedFile.Id));
                    else
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails("", ex.Message, data.RowIndex + 1, importedFile.Id));
                }
            }

            await _importedFileRepository.InsertDetailsAsync(importedFile.ImportedFileDetails);

            return;
        }

        public async Task<ResponseApi> GetAdminById(Guid id)
        {
            var authUser = await _userRepository.GetAdminById(id);

            if (authUser == null)
                return new ResponseApi(true, _localizer["AdminUser"], null);

            var details = await _detailsRepository.GetWithHealtUnityById(authUser.Id);
            var adminDetails = new AdminDetailsDto(authUser, details);

            return new ResponseApi(true, _localizer["AdminUser"], _mapper.Map<AdminDetailsViewModel>(adminDetails));
        }

        public async Task<ResponseApi> EditAdmin(AdminDto dto)
        {
            var currentAdminUser = await _userRepository.GetById(dto.Id.Value);
            if (currentAdminUser == null)
                throw new BusinessException(_localizer["UserNotFound"]);

            var currentAdminUserDetails = await _detailsRepository.Find(dto.Id.Value);
            if (currentAdminUserDetails == null)
                throw new BusinessException(_localizer["UserNotFound"]);

            await ValidateToSaveAdmin(dto);

            currentAdminUser.ChangeUser(dto);
            currentAdminUserDetails.ChangeUserDetail(dto);

            ValidateUserType(currentAdminUser, EUserType.Admin);

            UserToken currentUserActiveToken = null;
            if (!dto.IsActive.GetValueOrDefault() && currentAdminUser.IsActive(EUserType.Admin))
            {
                currentAdminUser.Deactivate(_accessor.GetCurrentUserId(), EUserType.Admin);

                currentUserActiveToken = await _userTokenRepository.GetActive(currentAdminUser.Id);
                currentUserActiveToken?.Deactivate();
            }

            if (dto.IsActive.GetValueOrDefault() && currentAdminUser.IsInactive(EUserType.Admin))
                currentAdminUser.Activate(EUserType.Admin);

            try
            {
                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                var result = await _userManager.UpdateAsync(currentAdminUser);

                if (!string.IsNullOrEmpty(dto.Password))
                    await ChangeUserPassword(currentAdminUser, dto.Password);

                ValidateSaveUserIdentityResult(result);

                if (!(await _detailsRepository.Update(currentAdminUserDetails)))
                    throw new BusinessException(_localizer["UserNotUpdated"]);

                if (currentUserActiveToken != null && !(await _userTokenRepository.Update(currentUserActiveToken)))
                    throw new BusinessException(_localizer["UserNotUpdated"]);

                _unitOfWork.CommitIdentity();
                _unitOfWork.CommitPassport();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackIdentity();
                _unitOfWork.RollbackPassport();

                VerifyUniqueKeyErrors(ex);

                throw;
            }

            return new ResponseApi(true, _localizer["UserUpdated"], currentAdminUser.Id);
        }

        private async Task ChangeUserPassword(Users user, string password)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var changeResult = await _userManager.ResetPasswordAsync(user, token, password);

            if (!changeResult.Succeeded)
                throw new BusinessException(_localizer["PasswordChangeError"]);
        }

        public async Task<PagedResponseApi> GetPagedAdmins(GetAdminUserPagedFilter filter)
        {
            var res = await _userRepository.GetPagedAdmins(filter, _accessor.GetAccessControlDTO());

            if (res.Data == null)
                return new PagedResponseApi(true, _localizer["AdminUsers"], 0, 0, 0, 0);

            return new PagedResponseApi(
                true,
                _localizer["AdminUsers"],
                res.PageNumber,
                res.PageSize,
                res.TotalPages,
                res.TotalRecords,
                _mapper.Map<IList<AdminUserViewModel>>(res.Data));
        }

        #region Private Methods
        private async Task<AccessControlDTO> GetCitizenControlData()
        {
            var accessControl = _accessor.GetAccessControlDTO();

            if (accessControl.Profile == EProfileKey.healthUnit.ToString())
                accessControl.FilterIds = await _detailsRepository.GetVaccinatedUsersWithHealtUnityById(accessControl.HealthUnityId.GetValueOrDefault());

            return accessControl;
        }

        private async Task ValidateAddCitizen(CitizenCreateDto dto)
        {
            if (dto.CompanyId.HasValue && await _companyRepository.Find(dto.CompanyId.Value) == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);

            if (dto.GenderId.HasValue && await _genderRepository.Find(dto.GenderId.Value) == null)
                throw new BusinessException(_localizer["GenderNotFound"]);

            if (dto.BloodTypeId.HasValue && await _bloodTypeRepository.Find(dto.BloodTypeId.Value) == null)
                throw new BusinessException(_localizer["BloodTypeNotFound"]);

            if (dto.HumanRaceId.HasValue && await _humanRaceRepository.Find(dto.HumanRaceId.Value) == null)
                throw new BusinessException(_localizer["HumanRaceNotFound"]);

            if (dto.PriorityGroupId.HasValue && await _priorityGroupRepository.Find(dto.PriorityGroupId.Value) == null)
                throw new BusinessException(_localizer["PriorityGroupNotFound"]);

            var citizenCity = await _cityRepository.FindLoadedById(dto.Address.CityId);

            if (dto.Address == null || citizenCity == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            if (dto.Test != null && dto.Test.IsEmpty())
                throw new BusinessException(_localizer["TestNotMustBeNullOrEmpty"]);

            if (dto.Doses != null && dto.Doses.Any())
            {
                if (!dto.Doses.All(x => x.VaccineId == dto.Doses.FirstOrDefault().VaccineId))
                    throw new BusinessException(_localizer["DifferentVaccinesDoses"]);

                ValidateVaccineDates(dto.Doses);

                foreach (var item in dto.Doses)
                {
                    if (await _healthUnitRepository.Find(item.HealthUnitId) == null)
                        throw new BusinessException(_localizer["HealthUnitNotFound"]);

                    if (dto.Doses.Any(x => x.VaccineId == item.VaccineId && x.Dose < item.Dose && x.VaccinationDate > item.VaccinationDate))
                        throw new BusinessException(_localizer["InvalidVaccineDoseDate"]);
                }
            }

            ValidateAddCitizenPermission(citizenCity);
        }

        private async Task ValidateEditCitizen(CitizenEditDto dto, AccessControlDTO accessControl, UserDetails userDetails, Users user)
        {
            ValidateUserType(user, EUserType.Citizen);

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

            if (dto.PriorityGroupId.HasValue && await _priorityGroupRepository.Find(dto.PriorityGroupId.Value) == null)
                throw new BusinessException(_localizer["PriorityGroupNotFound"]);

            var city = await _cityRepository.FindLoadedById(dto.Address.CityId);
            if (city == null)
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

            ValidateEditCitizenAccessControl(accessControl, city, userDetails, user, dto);
        }

        private void ValidateEditCitizenAccessControl(AccessControlDTO accessControl, City city, UserDetails userDetails, Users user, CitizenEditDto citizenEditDto)
        {
            //se perfil gov:
            // - mesma cidade/estado/país
            if (accessControl.Profile == EProfileKey.government.ToString()
                && ((accessControl.CityId.HasValue && accessControl.CityId.Value != city.Id)
                    || (accessControl.StateId.HasValue && accessControl.StateId.Value != city.StateId)
                    || (accessControl.CountryId.HasValue && accessControl.CountryId.Value != city.State.CountryId)))
                        throw new BusinessException(_localizer["OnlyAdminCanEditCitizenOutOfLocality"]);

            //se perfil unidade de saude
            // - mesma localidade ou vacinado na unidade
            if (accessControl.Profile == EProfileKey.healthUnit.ToString()
                && ((accessControl.CityId.HasValue && accessControl.CityId.Value != city.Id)
                    && accessControl.HealthUnityId.HasValue && !userDetails.UserVaccines.Any(x => x.HealthUnitId == accessControl.HealthUnityId.Value)))
                        throw new BusinessException(_localizer["OnlyAdminCanEditCitizenOutOfLocality"]);

            // validar campos permitidos na alteração
            if(!user.CanEditCitizenFields(citizenEditDto, accessControl) || !userDetails.CanEditCitizenFields(citizenEditDto, accessControl))
                throw new BusinessException(_localizer["ProfileNotAuthorizedToChangeRegistration"]);
        }

        private void ValidateSaveUserIdentityResult(IdentityResult result)
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
                    if (y.HealthUnit != null && y.HealthUnit.Address != null)
                    {
                        y.HealthUnit.Address = new AddressDto(_addressRepository.FindFullAddress(y.HealthUnit.Address.Id.Value).Result);
                    }
                    loadedDoses.Add(y);
                });
                x.Doses = loadedDoses;
                loadedUserVaccines.Add(x);
            });
            userVaccines = loadedUserVaccines;
        }

        private void ValidateExtractedData(List<CsvMappingResult<UserImportDto>> fileData, ImportedFile importedFile)
        {
            ValidationResult validationResult;

            fileData.Where(f => f.IsValid).ToList().ForEach(d =>
            {
                validationResult = new UserImportDtoValidator(_localizer).Validate(d.Result);
                if (!validationResult.IsValid)
                {
                    validationResult.Errors.ToList().ForEach(e =>
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + e.PropertyName], e.ErrorMessage, d.RowIndex, importedFile.Id))
                    );
                    d.Error = new CsvMappingError();
                }
            });
        }

        private IList<ImportedFileDetails> GetExtractionErrors(List<CsvMappingResult<UserImportDto>> fileData, Guid importedFileId)
        {
            return fileData.Where(f => !f.IsValid).Select(f => new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + ((EFileImportColumns)f.Error.ColumnIndex).ToString()], _localizer["InvalidValue"], f.RowIndex + 1, importedFileId)).ToList();
        }

        private List<CsvMappingResult<UserImportDto>> ReadCsvData(IFormFile file)
        {
            CsvParserOptions csvParserOptions = new(true, ';', 10, true);
            UserImportCsvMapper csvMapper = new();
            CsvParser<UserImportDto> csvParser = new(csvParserOptions, csvMapper);

            using MemoryStream ms = new();
            file.CopyTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return csvParser.ReadFromStream(ms, Encoding.UTF8).ToList();
        }

        private async Task GetComplementaryDatForUserImport(List<CsvMappingResult<UserImportDto>> validData, ImportedFile importedFile)
        {
            var genders = await _genderRepository.FindByListNames(validData.Select(f => f.Result.Gender.Trim().ToUpper()).Distinct().ToList());
            var companies = await _companyRepository.FindListCnpj(validData.Select(f => f.Result.Cnpj).Distinct().ToList());
            var priorityGroups = await _priorityGroupRepository.FindByListName(validData.Select(f => f.Result.PriorityGroup.Trim().ToUpper()).Distinct().ToList());
            var bloodTypes = await _bloodTypeRepository.FindByListName(validData.Select(f => f.Result.BloodType.Trim().ToUpper()).Distinct().ToList());
            var humanRaces = await _humanRaceRepository.FindByListName(validData.Select(f => f.Result.HumanRace.Trim().ToUpper()).Distinct().ToList());
            var cities = await _cityRepository.FindByCityStateAndCountryNames(validData.Select(f => f.Result.City.Trim().ToUpper() + f.Result.State.Trim().ToUpper() + f.Result.Country.Trim().ToUpper()).Distinct().ToList());
            var vaccines = await GetVaccinesToImportUser(validData);
            var healthUnits = await GetHealthUnitiesToImportUser(validData);

            Guid? id;

            validData.ForEach(v =>
            {
                id = genders.Where(g => g.Name.ToUpper() == v.Result.Gender.ToUpper()).Select(g => g.Id).SingleOrDefault();
                if (string.IsNullOrWhiteSpace(v.Result.Gender))
                {
                    v.Result.GenderId = null;
                }
                else if (id == Guid.Empty && !string.IsNullOrWhiteSpace(v.Result.Gender))
                {
                    importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.Gender.ToString()], _localizer["NonstandardField"], v.RowIndex + 1, importedFile.Id));
                    v.Error = new CsvMappingError();
                }
                else
                {
                    v.Result.GenderId = id;
                }

                id = companies.Where(c => c.Cnpj == v.Result.Cnpj).Select(c => c.Id).SingleOrDefault();
                if (string.IsNullOrWhiteSpace(v.Result.Cnpj))
                {
                    v.Result.CompanyId = null;
                }
                else if (id == Guid.Empty && !string.IsNullOrWhiteSpace(v.Result.Cnpj))
                {
                    importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.Cnpj.ToString()], _localizer["CnpjDoesntExistsInDataBase"], v.RowIndex + 1, importedFile.Id));
                    v.Error = new CsvMappingError();
                }
                else
                {
                    v.Result.CompanyId = id;
                }

                id = priorityGroups.Where(p => p.Name.ToUpper() == v.Result.PriorityGroup.ToUpper()).Select(p => p.Id).SingleOrDefault();
                if (string.IsNullOrWhiteSpace(v.Result.PriorityGroup))
                {
                    v.Result.PriorityGroupId = null;
                }
                else if (id == Guid.Empty && !string.IsNullOrWhiteSpace(v.Result.PriorityGroup))
                {
                    importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.PriorityGroup.ToString()], _localizer["NonstandardField"], v.RowIndex + 1, importedFile.Id));
                    v.Error = new CsvMappingError();
                }
                else
                {
                    v.Result.PriorityGroupId = id;
                }

                id = bloodTypes.Where(b => b.Name.ToUpper() == v.Result.BloodType.ToUpper()).Select(b => b.Id).SingleOrDefault();
                if (string.IsNullOrWhiteSpace(v.Result.BloodType))
                {
                    v.Result.BloodTypeId = null;
                }
                else if (id == Guid.Empty && !string.IsNullOrWhiteSpace(v.Result.BloodType))
                {
                    importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.BloodType.ToString()], _localizer["NonstandardField"], v.RowIndex + 1, importedFile.Id));
                    v.Error = new CsvMappingError();
                }
                else
                {
                    v.Result.BloodTypeId = id;
                }

                id = humanRaces.Where(h => h.Name.ToUpper() == v.Result.HumanRace.ToUpper()).Select(h => h.Id).SingleOrDefault();
                if (string.IsNullOrWhiteSpace(v.Result.HumanRace))
                {
                    v.Result.HumanRaceId = null;
                }
                else if (id == Guid.Empty && !string.IsNullOrWhiteSpace(v.Result.HumanRace))
                {
                    importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HumanRace.ToString()], _localizer["NonstandardField"], v.RowIndex + 1, importedFile.Id));
                    v.Error = new CsvMappingError();
                }
                else
                {
                    v.Result.HumanRaceId = id;
                }

                id = cities.Where(c => c.Name.ToUpper() == v.Result.City.ToUpper() && c.State.Name.ToUpper() == v.Result.State.ToUpper() && c.State.Country.Name.ToUpper() == v.Result.Country.ToUpper()).Select(c => c.Id).SingleOrDefault();
                if (id == Guid.Empty && !string.IsNullOrWhiteSpace(v.Result.City))
                {
                    importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.City.ToString()], _localizer["CityDoesntExistsInDataBase"], v.RowIndex + 1, importedFile.Id));
                    v.Error = new CsvMappingError();
                }
                else
                {
                    v.Result.CityId = id.Value;
                }

                ValidateVaccineUniqueDose(v, vaccines, healthUnits, importedFile);
                ValidateVaccineFirstDose(v, vaccines, healthUnits, importedFile);
                ValidateVaccineSecondDose(v, vaccines, healthUnits, importedFile);
                ValidateVaccineThirdDose(v, vaccines, healthUnits, importedFile);
            });
        }

        private async Task<IList<Vaccine>> GetVaccinesToImportUser(List<CsvMappingResult<UserImportDto>> validData)
        {
            var vaccineFilter = validData.Where(f => f.Result.HasVaccineUniqueDoseData).Select(f => f.Result.VaccineNameUniqueDose.Trim().ToUpper() + f.Result.VaccineManufacturerNameUniqueDose.Trim().ToUpper()).Distinct().ToList();
            vaccineFilter.AddRange(validData.Where(f => f.Result.HasVaccineFirstDoseData).Select(f => f.Result.VaccineNameFirstDose.Trim().ToUpper() + f.Result.VaccineManufacturerNameFirstDose.Trim().ToUpper()).Distinct().ToList());
            vaccineFilter.AddRange(validData.Where(f => f.Result.HasVaccineSecondDoseData).Select(f => f.Result.VaccineNameSecondDose.Trim().ToUpper() + f.Result.VaccineManufacturerNameSecondDose.Trim().ToUpper()).Distinct().ToList());
            vaccineFilter.AddRange(validData.Where(f => f.Result.HasVaccineThirdDoseData).Select(f => f.Result.VaccineNameThirdDose.Trim().ToUpper() + f.Result.VaccineManufacturerNameThirdDose.Trim().ToUpper()).Distinct().ToList());
            return await _vaccineRepository.GetByVaccineAndManufacturerNames(vaccineFilter.Distinct().ToList());
        }

        private async Task<IList<HealthUnit>> GetHealthUnitiesToImportUser(List<CsvMappingResult<UserImportDto>> validData)
        {
            List<GetHealthyUnityByCnpjIneAndUniqueCodeFilter> healthUnityFilter = new();
            healthUnityFilter.AddRange(validData.Where(f => f.Result.HasVaccineUniqueDoseData).Select(f => new GetHealthyUnityByCnpjIneAndUniqueCodeFilter { Cnpj = f.Result.HealthUnityCnpjUniqueDose, Ine = f.Result.HealthUnityIneUniqueDose, UniqueCode = f.Result.HealthUnityCodeUniqueDose }).Distinct().ToList());
            healthUnityFilter.AddRange(validData.Where(f => f.Result.HasVaccineFirstDoseData).Select(f => new GetHealthyUnityByCnpjIneAndUniqueCodeFilter { Cnpj = f.Result.HealthUnityCnpjFirstDose, Ine = f.Result.HealthUnityIneFirstDose, UniqueCode = f.Result.HealthUnityCodeFirstDose }).Distinct().ToList());
            healthUnityFilter.AddRange(validData.Where(f => f.Result.HasVaccineSecondDoseData).Select(f => new GetHealthyUnityByCnpjIneAndUniqueCodeFilter { Cnpj = f.Result.HealthUnityCnpjSecondDose, Ine = f.Result.HealthUnityIneSecondDose, UniqueCode = f.Result.HealthUnityCodeSecondDose }).Distinct().ToList());
            healthUnityFilter.AddRange(validData.Where(f => f.Result.HasVaccineThirdDoseData).Select(f => new GetHealthyUnityByCnpjIneAndUniqueCodeFilter { Cnpj = f.Result.HealthUnityCnpjThirdDose, Ine = f.Result.HealthUnityIneThirdDose, UniqueCode = f.Result.HealthUnityCodeThirdDose }).Distinct().ToList());
            return await _healthUnitRepository.FindByCnpjIneAndCode(healthUnityFilter.Where(h => !string.IsNullOrWhiteSpace(h.Cnpj)).Select(h => h.Cnpj).Distinct().ToList(),
                                                                   healthUnityFilter.Where(h => !string.IsNullOrWhiteSpace(h.Ine)).Select(h => h.Ine).Distinct().ToList(),
                                                                   healthUnityFilter.Where(h => h.UniqueCode.HasValue).Select(h => h.UniqueCode).Distinct().ToList());
        }

        private void ValidateVaccineUniqueDose(CsvMappingResult<UserImportDto> mappingResult, IList<Vaccine> vaccines, IList<HealthUnit> healthUnits, ImportedFile importedFile)
        {
            if (mappingResult.Result.HasVaccineUniqueDoseData)
            {
                Guid? id = vaccines.Where(vac => vac.Name.ToUpper() == mappingResult.Result.VaccineNameUniqueDose.ToUpper()
                                                                    && vac.Manufacturer.Name.ToUpper() == mappingResult.Result.VaccineManufacturerNameUniqueDose.ToUpper())
                                                        .Select(vac => vac.Id).SingleOrDefault();

                if (string.IsNullOrWhiteSpace(mappingResult.Result.VaccineNameUniqueDose) || string.IsNullOrWhiteSpace(mappingResult.Result.VaccineManufacturerNameUniqueDose))
                {
                    mappingResult.Result.VaccineIdUniqueDose = null;
                }
                else
                {
                    mappingResult.Result.VaccineIdUniqueDose = id;
                }

                var listHealthUnityIds = healthUnits.Where(h => (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjUniqueDose) || mappingResult.Result.HealthUnityCnpjUniqueDose == h.Cnpj)
                                                             && (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneUniqueDose) || mappingResult.Result.HealthUnityIneUniqueDose == h.Ine)
                                                            && (!mappingResult.Result.HealthUnityCodeUniqueDose.HasValue || mappingResult.Result.HealthUnityCodeUniqueDose == h.UniqueCode)).Select(h => h.Id).ToList();
                if (listHealthUnityIds.Count > 1)
                {
                    importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjUniqueDose.ToString()], _localizer["ThereIsMoreThanOneHealthUnityWithInformedData"], mappingResult.RowIndex + 1, importedFile.Id));
                    mappingResult.Error = new CsvMappingError();
                }
                else if (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjUniqueDose) && string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneUniqueDose) && !mappingResult.Result.HealthUnityCodeUniqueDose.HasValue)
                {
                    mappingResult.Result.HealthUnityIdUniqueDose = null;
                }
                else if (listHealthUnityIds.Count == 0 && (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjUniqueDose) || !string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneUniqueDose) || mappingResult.Result.HealthUnityCodeUniqueDose.HasValue))
                {
                    if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjUniqueDose) && !string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneUniqueDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjUniqueDose.ToString()], _localizer["CnpjIneDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjUniqueDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjUniqueDose.ToString()], _localizer["CnpjDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneUniqueDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityIneUniqueDose.ToString()], _localizer["IneDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCodeUniqueDose.ToString()], _localizer["HelthUnityCodeDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    mappingResult.Error = new CsvMappingError();
                }
                else
                {
                    mappingResult.Result.HealthUnityIdUniqueDose = listHealthUnityIds.First();
                }
            }
            else
            {
                mappingResult.Result.VaccineIdUniqueDose = null;
                mappingResult.Result.HealthUnityIdUniqueDose = null;
            }
        }

        private void ValidateVaccineFirstDose(CsvMappingResult<UserImportDto> mappingResult, IList<Vaccine> vaccines, IList<HealthUnit> healthUnits, ImportedFile importedFile)
        {
            if (mappingResult.Result.HasVaccineFirstDoseData)
            {
                Guid? id = vaccines.Where(vac => vac.Name.ToUpper() == mappingResult.Result.VaccineNameFirstDose.ToUpper()
                                                    && vac.Manufacturer.Name.ToUpper() == mappingResult.Result.VaccineManufacturerNameFirstDose.ToUpper()).Select(vac => vac.Id).SingleOrDefault();

                if (string.IsNullOrWhiteSpace(mappingResult.Result.VaccineNameFirstDose) || string.IsNullOrWhiteSpace(mappingResult.Result.VaccineManufacturerNameFirstDose))
                {
                    mappingResult.Result.VaccineIdFirstDose = null;
                }
                else
                {
                    mappingResult.Result.VaccineIdFirstDose = id;
                }

                var listHealthUnityIds = healthUnits.Where(h => (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjFirstDose) || mappingResult.Result.HealthUnityCnpjFirstDose == h.Cnpj)
                                                            && (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneFirstDose) || mappingResult.Result.HealthUnityIneFirstDose == h.Ine)
                                                            && (!mappingResult.Result.HealthUnityCodeFirstDose.HasValue || mappingResult.Result.HealthUnityCodeFirstDose == h.UniqueCode)).Select(h => h.Id).ToList();
                if (listHealthUnityIds.Count > 1)
                {
                    importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjFirstDose.ToString()], _localizer["ThereIsMoreThanOneHealthUnityWithInformedData"], mappingResult.RowIndex + 1, importedFile.Id));
                    mappingResult.Error = new CsvMappingError();
                }
                else if (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjFirstDose) && string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneFirstDose) && !mappingResult.Result.HealthUnityCodeFirstDose.HasValue)
                {
                    mappingResult.Result.HealthUnityIdFirstDose = null;
                }
                else if (listHealthUnityIds.Count == 0 && (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjFirstDose) || !string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneFirstDose) || mappingResult.Result.HealthUnityCodeFirstDose.HasValue))
                {
                    if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjFirstDose) && !string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneFirstDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjFirstDose.ToString()], _localizer["CnpjIneDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjFirstDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjFirstDose.ToString()], _localizer["CnpjDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneFirstDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityIneFirstDose.ToString()], _localizer["IneDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCodeFirstDose.ToString()], _localizer["HelthUnityCodeDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    mappingResult.Error = new CsvMappingError();
                }
                else
                {
                    mappingResult.Result.HealthUnityIdFirstDose = listHealthUnityIds.First();
                }
            }
            else
            {
                mappingResult.Result.VaccineIdFirstDose = null;
                mappingResult.Result.HealthUnityIdFirstDose = null;
            }
        }

        private void ValidateVaccineSecondDose(CsvMappingResult<UserImportDto> mappingResult, IList<Vaccine> vaccines, IList<HealthUnit> healthUnits, ImportedFile importedFile)
        {
            if (mappingResult.Result.HasVaccineSecondDoseData)
            {
                Guid? id = vaccines.Where(vac => vac.Name.ToUpper() == mappingResult.Result.VaccineNameSecondDose.ToUpper()
                                                                    && vac.Manufacturer.Name.ToUpper() == mappingResult.Result.VaccineManufacturerNameSecondDose.ToUpper()).Select(vac => vac.Id).SingleOrDefault();

                if (string.IsNullOrWhiteSpace(mappingResult.Result.VaccineNameSecondDose) || string.IsNullOrWhiteSpace(mappingResult.Result.VaccineManufacturerNameSecondDose))
                {
                    mappingResult.Result.VaccineIdSecondDose = null;
                }
                else
                {
                    mappingResult.Result.VaccineIdSecondDose = id;
                }

                var listHealthUnityIds = healthUnits.Where(h => (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjSecondDose) || mappingResult.Result.HealthUnityCnpjSecondDose == h.Cnpj)
                                                            && (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneSecondDose) || mappingResult.Result.HealthUnityIneSecondDose == h.Ine)
                                                            && (!mappingResult.Result.HealthUnityCodeSecondDose.HasValue || mappingResult.Result.HealthUnityCodeSecondDose == h.UniqueCode)).Select(h => h.Id).ToList();
                if (listHealthUnityIds.Count > 1)
                {
                    importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjSecondDose.ToString()], _localizer["ThereIsMoreThanOneHealthUnityWithInformedData"], mappingResult.RowIndex + 1, importedFile.Id));
                    mappingResult.Error = new CsvMappingError();
                }
                else if (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjSecondDose) && string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneSecondDose) && !mappingResult.Result.HealthUnityCodeSecondDose.HasValue)
                {
                    mappingResult.Result.HealthUnityIdSecondDose = null;
                }
                else if (listHealthUnityIds.Count == 0 && (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjSecondDose) || !string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneSecondDose) || mappingResult.Result.HealthUnityCodeSecondDose.HasValue))
                {
                    if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjSecondDose) && !string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneSecondDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjSecondDose.ToString()], _localizer["CnpjIneDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjSecondDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjSecondDose.ToString()], _localizer["CnpjDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneSecondDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityIneSecondDose.ToString()], _localizer["IneDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCodeSecondDose.ToString()], _localizer["HelthUnityCodeDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    mappingResult.Error = new CsvMappingError();
                }
                else
                {
                    mappingResult.Result.HealthUnityIdSecondDose = listHealthUnityIds.First();
                }
            }
            else
            {
                mappingResult.Result.VaccineIdSecondDose = null;
                mappingResult.Result.HealthUnityIdSecondDose = null;
            }
        }

        private void ValidateVaccineThirdDose(CsvMappingResult<UserImportDto> mappingResult, IList<Vaccine> vaccines, IList<HealthUnit> healthUnits, ImportedFile importedFile)
        {
            if (mappingResult.Result.HasVaccineThirdDoseData)
            {
                Guid? id = vaccines.Where(vac => vac.Name.ToUpper() == mappingResult.Result.VaccineNameThirdDose.ToUpper()
                                                                && vac.Manufacturer.Name.ToUpper() == mappingResult.Result.VaccineManufacturerNameThirdDose.ToUpper()).Select(vac => vac.Id).SingleOrDefault();

                if (string.IsNullOrWhiteSpace(mappingResult.Result.VaccineNameThirdDose) || string.IsNullOrWhiteSpace(mappingResult.Result.VaccineManufacturerNameThirdDose))
                {
                    mappingResult.Result.VaccineIdThirdDose = null;
                }
                else
                {
                    mappingResult.Result.VaccineIdThirdDose = id;
                }

                var listHealthUnityIds = healthUnits.Where(h => (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjThirdDose) || mappingResult.Result.HealthUnityCnpjThirdDose == h.Cnpj)
                                                              && (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneThirdDose) || mappingResult.Result.HealthUnityIneThirdDose == h.Ine)
                                                              && (!mappingResult.Result.HealthUnityCodeThirdDose.HasValue || mappingResult.Result.HealthUnityCodeThirdDose == h.UniqueCode)).Select(h => h.Id).ToList();

                if (listHealthUnityIds.Count > 1)
                {
                    importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjThirdDose.ToString()], _localizer["ThereIsMoreThanOneHealthUnityWithInformedData"], mappingResult.RowIndex + 1, importedFile.Id));
                    mappingResult.Error = new CsvMappingError();
                }
                else if (string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjThirdDose) && string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneThirdDose) && !mappingResult.Result.HealthUnityCodeThirdDose.HasValue)
                {
                    mappingResult.Result.HealthUnityIdThirdDose = null;
                }
                else if (listHealthUnityIds.Count == 0 && (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjThirdDose) || !string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneThirdDose) || mappingResult.Result.HealthUnityCodeThirdDose.HasValue))
                {
                    if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjThirdDose) && !string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneThirdDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjThirdDose.ToString()], _localizer["CnpjIneDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityCnpjThirdDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCnpjThirdDose.ToString()], _localizer["CnpjDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else if (!string.IsNullOrWhiteSpace(mappingResult.Result.HealthUnityIneThirdDose))
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityIneThirdDose.ToString()], _localizer["IneDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    else
                    {
                        importedFile.ImportedFileDetails.Add(new ImportedFileDetails(_localizer[Domain.Utils.Constants.COLUMN_NAME_IMPORT_FILE_TO_RESOURCE + EFileImportColumns.HealthUnityCodeThirdDose.ToString()], _localizer["HelthUnityCodeDoesntExistsInDataBase"], mappingResult.RowIndex + 1, importedFile.Id));
                    }
                    mappingResult.Error = new CsvMappingError();
                }
                else
                {
                    mappingResult.Result.HealthUnityIdThirdDose = listHealthUnityIds.First();
                }
            }
            else
            {
                mappingResult.Result.VaccineIdThirdDose = null;
                mappingResult.Result.HealthUnityIdThirdDose = null;
            }
        }

        private void ValidateFileLenght(IFormFile file)
        {
            if (file.Length > Domain.Utils.Constants.MAX_LENGHT_IMPORT_USERS_FILE)
                throw new BusinessException(string.Format(_localizer["ImportUsersFileMaxSize"], Domain.Utils.Constants.MAX_LENGHT_IMPORT_USERS_FILE / 1024));
        }

        private async Task<bool> ValidateToSaveAdmin(AdminDto dto)
        {
            if (!dto.CompanyId.HasValue || await _companyRepository.Find(dto.CompanyId.Value) == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);

            var Profile = await _profileRepository.Find(dto.ProfileId.GetValueOrDefault());
            if (Profile == null)
                throw new BusinessException(_localizer["ProfileNotFound"]);

            if (Profile.Key == Enum.GetName(EProfileKey.healthUnit)
                && (!dto.HealthUnitId.HasValue || await _healthUnitRepository.Find(dto.HealthUnitId.Value) == null))
                throw new BusinessException(string.Format(_localizer["HealthUnitRequiredToProfile"], Profile.Name));

            if (dto.HealthUnitId.HasValue && Profile.Key != Enum.GetName(EProfileKey.healthUnit))
                throw new BusinessException(string.Format(_localizer["HealthUnitMustNotBeInsertedToProfile"], Profile.Name));

            return true;
        }

        private void VerifyUniqueKeyErrors(Exception ex)
        {
            if (ex.ToString().Contains("IX_Users_CNS"))
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CNS"));
            if (ex.ToString().Contains("IX_Users_CPF"))
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CPF"));
            if (ex.ToString().Contains("IX_Users_RG"))
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "RG"));
            if (ex.ToString().Contains("IX_Users_InternationalDocument"))
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], _localizer["InternationalDocument"]));
            if (ex.ToString().Contains("IX_Users_PassportDoc"))
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], _localizer["PassportDoc"]));
            if (ex.ToString().Contains("IX_Users_Email"))
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], _localizer["Email"]));
            if (ex.ToString().Contains("IX_Users_PhoneNumber"))
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], _localizer["Telephone"]));
        }

        private void ValidateAddCitizenPermission(City citizenCity)
        {
            var acessControll = _accessor.GetAccessControlDTO();
            if ((acessControll.Profile == EProfileKey.government.ToString()) &&
                ((acessControll.CityId.HasValue && acessControll.CityId.Value != citizenCity.Id) ||
                    (acessControll.StateId.HasValue && acessControll.StateId.Value != citizenCity?.StateId) ||
                    (acessControll.CountryId.HasValue && acessControll.CountryId.Value != citizenCity?.State.CountryId)))
                throw new BusinessException(_localizer["LoggedInUserCanOnlyRegisterCitizensWithSameLocationAsHis"]);
        }

        private async Task<Guid> GetUserTypeIdByIdentifierWhenExists(EUserType userTypeIdentifyer)
        {
            var userType = await _userTypeRepository.GetByIdentifier((int)userTypeIdentifyer);
            if (userType == null)
                throw new BusinessException(_localizer["UserTypeNotFound"]);

            return userType.Id;
        }

        private void ValidateUserType(Users editedUser, EUserType userTypeIdentifyer)
        {
            if(!editedUser.HasType(userTypeIdentifyer))
                throw new BusinessException(string.Format(_localizer["UserNotHaveAccessOfType"],_localizer[userTypeIdentifyer.ToString()]));
        }

        #endregion
    }
}
