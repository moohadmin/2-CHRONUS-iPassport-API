using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class UserServiceTest
    {
        Mock<IUserDetailsRepository> _mockRepository;
        Mock<IUserRepository> _mockUserRepository;
        Mock<IPlanRepository> _planMockRepository;
        IUserService _service;
        IMapper _mapper;
        Mock<UserManager<Users>> _mockUserManager;
        IHttpContextAccessor _accessor;
        Mock<IStorageExternalService> _externalStorageService;
        Mock<ICompanyRepository> _mockCompanyRepository;
        Mock<ICityRepository> _mockCityRepository;
        Mock<IVaccineRepository> _mockVaccineRepository;
        Mock<IGenderRepository> _mockGenderRepository;
        Mock<IBloodTypeRepository> _mockBloodTypeRepository;
        Mock<IHumanRaceRepository> _mockHumanRaceRepository;
        Mock<IPriorityGroupRepository> _mockPriorityGroupRepository;
        Mock<IHealthUnitRepository> _mockHealthUnitRepository;
        Mock<IUserVaccineRepository> _mockUserVaccineRepository;
        Mock<IUserDiseaseTestRepository> _mockUserDiseaseTestRepository;
        Mock<IAddressRepository> _mockAddressRepository;
        Mock<IUnitOfWork> _mockUnitOfWork;
        Mock<IImportedFileRepository> _mockImportedFileRepository;
        Mock<IProfileRepository> _mockProfileRepository;
        Mock<IUserTokenRepository> _mockUserTokenRepository;
        Resource _resource;
        Mock<IUserTypeRepository> _mockUserTypeRepository;

        [TestInitialize]
        public void Setup()
        {
            _resource = ResourceFactory.Create();
            _mapper = AutoMapperFactory.Create();
            _accessor = HttpContextAccessorFactory.Create();
            _mockRepository = new Mock<IUserDetailsRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _planMockRepository = new Mock<IPlanRepository>();
            _externalStorageService = new Mock<IStorageExternalService>();
            _mockUserManager = UserManagerFactory.CreateMock();
            _mockCompanyRepository = new Mock<ICompanyRepository>();
            _mockCityRepository = new Mock<ICityRepository>();
            _mockVaccineRepository = new Mock<IVaccineRepository>();
            _mockGenderRepository = new Mock<IGenderRepository>();
            _mockBloodTypeRepository = new Mock<IBloodTypeRepository>();
            _mockHumanRaceRepository = new Mock<IHumanRaceRepository>();
            _mockPriorityGroupRepository = new Mock<IPriorityGroupRepository>();
            _mockHealthUnitRepository = new Mock<IHealthUnitRepository>();
            _mockUserVaccineRepository = new Mock<IUserVaccineRepository>();
            _mockUserDiseaseTestRepository = new Mock<IUserDiseaseTestRepository>();
            _mockAddressRepository = new Mock<IAddressRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockImportedFileRepository = new();
            _mockProfileRepository = new Mock<IProfileRepository>();
            _mockUserTokenRepository = new Mock<IUserTokenRepository>();
            _mockUserTypeRepository = new Mock<IUserTypeRepository>();

            _service = new UserService(_mockUserRepository.Object, _mockRepository.Object, _planMockRepository.Object, _mapper, _accessor, _mockUserManager.Object, _externalStorageService.Object, ResourceFactory.GetStringLocalizer(), _mockCompanyRepository.Object, _mockCityRepository.Object, _mockVaccineRepository.Object,
                _mockGenderRepository.Object, _mockBloodTypeRepository.Object, _mockHumanRaceRepository.Object, _mockPriorityGroupRepository.Object, _mockHealthUnitRepository.Object,
                _mockUserVaccineRepository.Object, _mockUserDiseaseTestRepository.Object, _mockAddressRepository.Object, _mockUnitOfWork.Object, _mockImportedFileRepository.Object,
                _mockProfileRepository.Object, _mockUserTokenRepository.Object, _mockUserTypeRepository.Object);
        }

        [TestMethod]
        public void AssociatePlan()
        {
            var userSeed = UserSeed.GetUserDetails();
            var planSeed = PlanSeed.GetPlans().FirstOrDefault();
            var mockRequest = Guid.NewGuid();

            // Arrange
            _mockRepository.Setup(r => r.Update(It.IsAny<UserDetails>()).Result).Returns(true);
            _mockRepository.Setup(r => r.GetByUserId(It.IsAny<Guid>()).Result).Returns(userSeed);
            _planMockRepository.Setup(r => r.Find(It.IsAny<Guid>()).Result).Returns(planSeed);

            // Act
            var result = _service.AssociatePlan(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.Update(It.IsAny<UserDetails>()), Times.Once);
            _mockRepository.Verify(a => a.GetByUserId(It.IsAny<Guid>()), Times.Once);
            _planMockRepository.Verify(a => a.Find(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetUserPlan()
        {
            var userSeed = UserSeed.GetUserDetails();

            // Arrange
            _mockRepository.Setup(r => r.GetByUserId(It.IsAny<Guid>()).Result).Returns(userSeed);

            // Act
            var result = _service.GetUserPlan();

            // Assert
            _mockRepository.Verify(a => a.GetByUserId(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(PlanViewModel));
        }

        [TestMethod]
        public void GetCurrentUser()
        {
            // Arrange
            _mockUserRepository.Setup(x => x.GetById(It.IsAny<Guid>()).Result).Returns(UserSeed.Get(EUserType.Citizen));

            // Act
            var result = _service.GetCurrentUser("small");

            // Assert
            _mockUserRepository.Verify(a => a.GetById(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(UserDetailsViewModel));
        }

        [TestMethod]
        public void AddUserImage_SavesPhotoUrlIntoUserDetails()
        {
            var mockRequest = Mock.Of<UserImageDto>();
            mockRequest.ImageFile = Mock.Of<IFormFile>();
            var SaveUrl = "./Content/Image/Teste.jpg";

            // Arrange
            _externalStorageService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()).Result).Returns(SaveUrl);
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()).Result).Returns(UserSeed.GetUser());

            // Act
            var result = _service.AddUserImage(mockRequest);

            // Assert
            _externalStorageService.Verify(a => a.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(string));

        }

        [TestMethod]
        public void RemoveUserImage_RemovePhotoFromUserDetails()
        {
            var authSeed = UserSeed.GetUserWithPhoto();
            var mockUserId = Guid.NewGuid();

            // Arrange
            _externalStorageService.Setup(x => x.DeleteFileAsync(It.IsAny<string>()));
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()).Result).Returns(authSeed);

            // Act
            var result = _service.RemoveUserImage(mockUserId);

            // Assert
            _externalStorageService.Verify(a => a.DeleteFileAsync(It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNull(result.Result.Data);
            Assert.IsNull(authSeed.Photo);
        }

        [TestMethod]
        public void GetLoggedCitzenCount()
        {
            var seed = new Random().Next(99999);

            // Arrange
            _mockUserRepository.Setup(r => r.GetLoggedCitzenCount().Result).Returns(seed);

            // Act
            var result = _service.GetLoggedCitzenCount();

            // Assert
            _mockUserRepository.Verify(a => a.GetLoggedCitzenCount(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(int));
            Assert.AreEqual(seed, result.Result.Data);
        }

        [TestMethod]
        public void GetRegisteredUserCount()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetRegisteredUserCount(It.IsAny<GetRegisteredUserCountFilter>()).Result).Returns(5);

            // Act
            var result = _service.GetRegisteredUserCount(It.IsAny<GetRegisteredUserCountFilter>());

            // Assert
            _mockUserRepository.Verify(a => a.GetRegisteredUserCount(It.IsAny<GetRegisteredUserCountFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.AreEqual(5, result.Result.Data);
        }

        [TestMethod]
        public void GetLoggedAgentCount()
        {
            var seed = new Random().Next(99999);

            // Arrange
            _mockUserRepository.Setup(r => r.GetLoggedAgentCount().Result).Returns(seed);

            // Act
            var result = _service.GetLoggedAgentCount();

            // Assert
            _mockUserRepository.Verify(a => a.GetLoggedAgentCount(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(int));
            Assert.AreEqual(seed, result.Result.Data);
        }

        [TestMethod]
        public void GetPagedAgent()
        {
            // Arrange
            var mockRequest = Mock.Of<GetAgentPagedFilter>();

            _mockUserRepository.Setup(x => x.GetPagedAgent(It.IsAny<GetAgentPagedFilter>(), It.IsAny<AccessControlDTO>()).Result)
                .Returns(UserSeed.GetPagedUsers());

            // Act
            var result = _service.GetPagedAgent(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetPagedAgent(It.IsAny<GetAgentPagedFilter>(), It.IsAny<AccessControlDTO>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        [DataRow("test agent")]
        [DataRow("test agent", "test.agent")]
        [DataRow("test cerqueira agent", "test.agent")]
        [DataRow("test caratere êéspeÇiàáãlúû", "test.eespeciaaaluu")]
        public void AddAgent(string fullName, string repeatedName = null)
        {
            var mockRequest = Mock.Of<UserAgentDto>(x => x.Address == Mock.Of<AddressDto>(y => y.CityId == Guid.NewGuid()) && x.FullName == fullName);
            var identyResult = Mock.Of<IdentityResult>(x => x.Succeeded == true);

            // Arrange
            if (repeatedName != null)
                _mockUserRepository.Setup(x => x.GetUsernamesList(It.IsAny<IList<string>>()).Result).Returns(new List<string>() { repeatedName });
            else
                _mockUserRepository.Setup(x => x.GetUsernamesList(It.IsAny<IList<string>>()).Result);


            _mockCompanyRepository.Setup(x => x.GetPrivateActiveCompanies(It.IsAny<Guid>()).Result).Returns(CompanySeed.GetCompanies());
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Users>(), It.IsAny<string>()).Result).Returns(identyResult);
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<UserDetails>()));
            _mockUserTypeRepository.Setup(x => x.GetByIdentifier(It.IsAny<int>()).Result).Returns(UserTypeSeed.GetAgent());

            // Act
            var result = _service.AddAgent(mockRequest);

            // Assert
            _mockCompanyRepository.Verify(x => x.GetPrivateActiveCompanies(It.IsAny<Guid>()));
            _mockCityRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockUserManager.Verify(x => x.CreateAsync(It.IsAny<Users>(), It.IsAny<string>()));
            _mockRepository.Verify(x => x.InsertAsync(It.IsAny<UserDetails>()));

            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(Guid));
        }

        [TestMethod]
        public void EditAgent()
        {
            // Arrange
            var mockRequest = Mock.Of<UserAgentDto>(x => x.CompanyId == Guid.NewGuid() && x.Id == Guid.NewGuid() && x.Address == Mock.Of<AddressDto>(y => y.CityId == Guid.NewGuid()));

            _mockUserRepository.Setup(x => x.GetById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserAdmin());
            _mockRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockCompanyRepository.Setup(x => x.GetPrivateActiveCompanies(It.IsAny<Guid>()).Result).Returns(CompanySeed.GetCompanies());
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockUserRepository.Setup(x => x.Update(It.IsAny<Users>())).Returns(Task.CompletedTask);

            // Act
            var result = _service.EditAgent(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetById(It.IsAny<Guid>()));
            _mockRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockCompanyRepository.Verify(x => x.GetPrivateActiveCompanies(It.IsAny<Guid>()));
            _mockCityRepository.Verify(x => x.Find(It.IsAny<Guid>()));

            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        [DataRow("Test!343", true)]
        [DataRow("Testt343", false)]
        [DataRow("test!343", false)]
        [DataRow("invalido!!!!", false)]
        public void EditAgentChangePassword(string password, bool isValid)
        {
            // Arrange
            var mockRequest = Mock.Of<UserAgentDto>(x => x.CompanyId == Guid.NewGuid() && x.Id == Guid.NewGuid() && x.Address == Mock.Of<AddressDto>(y => y.CityId == Guid.NewGuid()) && x.Password == password);
            var identityResult = Mock.Of<IdentityResult>(x => x.Succeeded == isValid);

            _mockUserRepository.Setup(x => x.GetById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserAdmin());
            _mockRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockCompanyRepository.Setup(x => x.GetPrivateActiveCompanies(It.IsAny<Guid>()).Result).Returns(CompanySeed.GetCompanies());
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockUserRepository.Setup(x => x.Update(It.IsAny<Users>())).Returns(Task.CompletedTask);
            _mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<Users>()).Result).Returns("test");
            _mockUserManager.Setup(x => x.ResetPasswordAsync(It.IsAny<Users>(), It.IsAny<string>(), password).Result).Returns(identityResult);

            // Assert
            if (isValid)
            {
                var result = _service.EditAgent(mockRequest);
                _mockUserRepository.Verify(x => x.GetById(It.IsAny<Guid>()));
                Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
                Assert.IsNotNull(result.Result.Data);
            }
            else
            {
                var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EditAgent(mockRequest)).Result;
                string message = _resource.GetMessage("PasswordChangeError");

                Assert.AreEqual(message, ex.Message);
            }

            _mockRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockCompanyRepository.Verify(x => x.GetPrivateActiveCompanies(It.IsAny<Guid>()));
            _mockCityRepository.Verify(x => x.Find(It.IsAny<Guid>()));
        }

        [TestMethod]
        public void FindCitizensByNameParts()
        {
            // Arrange
            var mockRequest = Mock.Of<GetCitzenPagedFilter>();

            _mockUserRepository.Setup(x => x.GetPaggedCizten(It.IsAny<GetCitzenPagedFilter>(), It.IsAny<AccessControlDTO>()).Result)
                .Returns(UserSeed.GetPagedUsers());
            _mockRepository.Setup(x => x.GetImportedUserById(It.IsAny<Guid[]>()).Result)
                .Returns(UserSeed.GetImportedUserDto());

            // Act
            var result = _service.GetPaggedCizten(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetPaggedCizten(It.IsAny<GetCitzenPagedFilter>(), It.IsAny<AccessControlDTO>()));
            _mockRepository.Verify(x => x.GetImportedUserById(It.IsAny<Guid[]>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetCitizenById()
        {
            // Arrange
            var mockRequest = Guid.NewGuid();

            _mockUserRepository.Setup(x => x.GetLoadedCitizenById(It.IsAny<Guid>()).Result)
                .Returns(UserSeed.GetUsers().FirstOrDefault());
            _mockRepository.Setup(r => r.GetLoadedUserById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockAddressRepository.Setup(r => r.Find(It.IsAny<Guid>()).Result).Returns(AddressSeed.Get());

            // Act
            var result = _service.GetCitizenById(mockRequest, "small");

            // Assert
            _mockUserRepository.Verify(x => x.GetLoadedCitizenById(It.IsAny<Guid>()));
            _mockRepository.Verify(x => x.GetLoadedUserById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void EditCitizen()
        {
            // Arrange
            var mockRequest = Mock.Of<CitizenEditDto>(x => x.PriorityGroupId == Guid.NewGuid() && x.Address == Mock.Of<AddressEditDto>());
            var identityResult = Mock.Of<IdentityResult>(x => x.Succeeded == true);

            _mockUserRepository.Setup(x => x.GetById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserCitizen());
            _mockRepository.Setup(r => r.GetLoadedUserById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockAddressRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(AddressSeed.Get());
            _mockCityRepository.Setup(x => x.FindLoadedById(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockPriorityGroupRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(PriorityGroupSeed.Get());
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<Users>()).Result).Returns(identityResult);
            _mockRepository.Setup(x => x.Update(It.IsAny<UserDetails>()).Result).Returns(true);
            _mockUserVaccineRepository.Setup(x => x.Update(It.IsAny<UserVaccine>()).Result).Returns(true);
            _mockUserDiseaseTestRepository.Setup(x => x.Delete(It.IsAny<UserDiseaseTest>()).Result).Returns(true);
            // Act
            var result = _service.EditCitizen(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetById(It.IsAny<Guid>()));
            _mockRepository.Verify(x => x.GetLoadedUserById(It.IsAny<Guid>()));
            _mockAddressRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockCityRepository.Verify(x => x.FindLoadedById(It.IsAny<Guid>()));
            _mockPriorityGroupRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockUserManager.Verify(x => x.UpdateAsync(It.IsAny<Users>()));
            _mockRepository.Verify(x => x.Update(It.IsAny<UserDetails>()));
            _mockUserDiseaseTestRepository.Verify(x => x.Delete(It.IsAny<UserDiseaseTest>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void AddAdmin()
        {
            // Arrange
            var mockRequest = Mock.Of<AdminDto>(x => x.CompanyId == Guid.NewGuid() && x.IsActive == true);
            var identityResult = Mock.Of<IdentityResult>(x => x.Succeeded == true);
            _mockCompanyRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CompanySeed.Get());
            _mockProfileRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(ProfileSeed.Get());
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Users>(), It.IsAny<string>()).Result).Returns(identityResult);
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<UserDetails>()));
            _mockUserTypeRepository.Setup(x => x.GetByIdentifier((int)EUserType.Admin).Result).Returns(UserTypeSeed.GetAdmin());

            // Act
            var result = _service.AddAdmin(mockRequest);

            // Assert
            _mockCompanyRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockProfileRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockUserManager.Verify(x => x.CreateAsync(It.IsAny<Users>(), It.IsAny<string>()));
            _mockRepository.Verify(x => x.InsertAsync(It.IsAny<UserDetails>()));
            _mockUserTypeRepository.Verify(x => x.GetByIdentifier((int)EUserType.Admin));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetAdminById()
        {
            // Arrange
            var mockRequest = Guid.NewGuid();
            var userDetails = UserSeed.GetUserDetails();
            userDetails.HealthUnit.Type = new HealthUnitType();

            _mockUserRepository.Setup(x => x.GetAdminById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUser());
            _mockRepository.Setup(r => r.GetWithHealtUnityById(It.IsAny<Guid>()).Result).Returns(userDetails);

            // Act
            var result = _service.GetAdminById(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetAdminById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetPagedAdmins()
        {
            // Arrange
            var mockRequest = Mock.Of<GetAdminUserPagedFilter>();

            _mockUserRepository.Setup(x => x.GetPagedAdmins(It.IsAny<GetAdminUserPagedFilter>(), It.IsAny<AccessControlDTO>()).Result)
                .Returns(UserSeed.GetPagedUsers());

            // Act
            var result = _service.GetPagedAdmins(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetPagedAdmins(It.IsAny<GetAdminUserPagedFilter>(), It.IsAny<AccessControlDTO>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void EditAdmin()
        {
            // Arrange
            var mockRequest = Mock.Of<AdminDto>(x => x.CompanyId == Guid.NewGuid() && x.Id == Guid.NewGuid());
            var identityResult = Mock.Of<IdentityResult>(x => x.Succeeded == true);
            _mockUserRepository.Setup(x => x.GetById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserAdmin());
            _mockRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockCompanyRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CompanySeed.Get());
            _mockProfileRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(ProfileSeed.Get());
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<Users>()).Result).Returns(identityResult);
            _mockRepository.Setup(x => x.Update(It.IsAny<UserDetails>()).Result).Returns(true);

            // Act
            var result = _service.EditAdmin(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetById(It.IsAny<Guid>()));
            _mockRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockCompanyRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockProfileRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockUserManager.Verify(x => x.UpdateAsync(It.IsAny<Users>()));
            _mockRepository.Verify(x => x.Update(It.IsAny<UserDetails>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        [DataRow("2021-02-15T17:03:34.832Z", "2021-02-15T17:03:34.832Z")]
        [DataRow("2021-02-15T17:03:34.832Z", "2021-02-14T17:03:34.832Z")]
        public void EditCitizen_invalidDosesDate(string dateString1, string dateString2)
        {
            var date1 = DateTime.Parse(dateString1);
            var date2 = DateTime.Parse(dateString2);

            // Arrange
            var mockRequest = Mock.Of<CitizenEditDto>(x =>
                x.PriorityGroupId == Guid.NewGuid()
                && x.Address == Mock.Of<AddressEditDto>()
                && x.NumberOfDoses == 2
                && x.Doses == new List<UserVaccineEditDto>()
                {
                    Mock.Of<UserVaccineEditDto>(y => y.Dose == 1 && y.VaccinationDate == date1),
                    Mock.Of<UserVaccineEditDto>(y => y.Dose == 2 && y.VaccinationDate == date2),

                });

            // Act
            _mockUserRepository.Setup(x => x.GetById(It.IsAny<Guid>()).Result).Returns(UserSeed.Get(EUserType.Citizen));
            _mockRepository.Setup(r => r.GetLoadedUserById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockAddressRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(AddressSeed.Get());
            _mockCityRepository.Setup(x => x.FindLoadedById(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockPriorityGroupRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(PriorityGroupSeed.Get());
            _mockVaccineRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(VaccineSeed.GetVaccines().FirstOrDefault());
            _mockHealthUnitRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(HealthUnitSeed.GetHealthUnit());
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<Users>()).Result).Returns(Mock.Of<IdentityResult>(x => x.Succeeded));
            _mockRepository.Setup(x => x.Update(It.IsAny<UserDetails>()).Result).Returns(true);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EditCitizen(mockRequest)).Result;
            string message;

            if (date1.Date == date2.Date)
                message = _resource.GetMessage("VaccineDoseDateCannoteBeEquals");

            else
            {
                message = _resource.GetMessage("VaccineNextDoseDateCannoteBeLowerToPrevious");
            }
            Assert.AreEqual(message, ex.Message);
        }

        [TestMethod]
        public void GetAgentById()
        {
            // Arrange
            var mockRequest = Guid.NewGuid();
            _mockUserRepository.Setup(x => x.GetAgentById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserAdmin());

            // Act
            var result = _service.GetAgentById(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetAgentById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(AgentDetailsViewModel));
        }
    }
}
