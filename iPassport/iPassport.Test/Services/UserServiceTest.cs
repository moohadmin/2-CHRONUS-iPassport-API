using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
        Mock<IStringLocalizer<Resource>> _mockLocalizer;
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

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _accessor = HttpContextAccessorFactory.Create();
            _mockRepository = new Mock<IUserDetailsRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _planMockRepository = new Mock<IPlanRepository>();
            _externalStorageService = new Mock<IStorageExternalService>();
            _mockUserManager = UserManagerFactory.CreateMock();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
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

            _service = new UserService(_mockUserRepository.Object, _mockRepository.Object, _planMockRepository.Object, _mapper, _accessor, _mockUserManager.Object, _externalStorageService.Object, _mockLocalizer.Object, _mockCompanyRepository.Object, _mockCityRepository.Object, _mockVaccineRepository.Object,
                _mockGenderRepository.Object, _mockBloodTypeRepository.Object, _mockHumanRaceRepository.Object, _mockPriorityGroupRepository.Object, _mockHealthUnitRepository.Object,
                _mockUserVaccineRepository.Object, _mockUserDiseaseTestRepository.Object, _mockAddressRepository.Object, _mockUnitOfWork.Object);
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
            var detailsSeed = UserSeed.GetUserDetails();
            
            // Arrange
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()).Result).Returns(UserSeed.GetUser());

            // Act
            var result = _service.GetCurrentUser();

            // Assert
            _mockUserManager.Verify(a => a.FindByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(UserDetailsViewModel));
        }

        [TestMethod]
        public void AddUserImage_SavesPhotoUrlIntoUserDetails()
        {
            var authSeed = UserSeed.GetUser();
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
        public void AddAgent()
        {
            var mockRequest = Mock.Of<UserAgentCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>());
            var identyResult = Mock.Of<IdentityResult>(x => x.Succeeded == true);
            // Arrange
            _mockCompanyRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CompanySeed.Get());
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Users>(), It.IsAny<string>()).Result).Returns(identyResult);
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<UserDetails>()));
            // Act
            var result = _service.AddAgent(mockRequest);

            // Assert
            _mockCompanyRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockCityRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockUserManager.Verify(x => x.CreateAsync(It.IsAny<Users>(), It.IsAny<string>()));
            _mockRepository.Verify(x => x.InsertAsync(It.IsAny<UserDetails>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(Guid));
        }

        [TestMethod]
        public void FindCitizensByNameParts()
        {
            // Arrange
            var mockRequest = Mock.Of<GetCitzenPagedFilter>();

            _mockUserRepository.Setup(x => x.GetPaggedCizten(It.IsAny<GetCitzenPagedFilter>()).Result)
                .Returns(UserSeed.GetPagedUsers());

            // Act
            var result = _service.GetPaggedCizten(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetPaggedCizten(It.IsAny<GetCitzenPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetCitizenById()
        {
            // Arrange
            var mockRequest = Guid.NewGuid();

            _mockUserRepository.Setup(x => x.GetLoadedUsersById(It.IsAny<Guid>()).Result)
                .Returns(UserSeed.GetUsers().FirstOrDefault());
            _mockRepository.Setup(r => r.GetLoadedUserById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());

            // Act
            var result = _service.GetCitizenById(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetLoadedUsersById(It.IsAny<Guid>()));
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

            _mockUserRepository.Setup(x => x.GetById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUser());            
            _mockRepository.Setup(r => r.GetLoadedUserById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockAddressRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(AddressSeed.Get());
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockPriorityGroupRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(PriorityGroupSeed.Get());
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<Users>()).Result).Returns(identityResult);
            _mockRepository.Setup(x => x.Update(It.IsAny<UserDetails>()).Result).Returns(true);
            _mockUserVaccineRepository.Setup(x => x.Update(It.IsAny<UserVaccine>()).Result).Returns(true);
            // Act
            var result = _service.EditCitizen(mockRequest);

            // Assert
            _mockUserRepository.Verify(x => x.GetById(It.IsAny<Guid>()));
            _mockRepository.Verify(x => x.GetLoadedUserById(It.IsAny<Guid>()));
            _mockAddressRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockCityRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockPriorityGroupRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockUserManager.Verify(x => x.UpdateAsync(It.IsAny<Users>()));
            _mockRepository.Verify(x => x.Update(It.IsAny<UserDetails>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}
