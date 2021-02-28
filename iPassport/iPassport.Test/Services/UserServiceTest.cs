using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using iPassport.Domain.Repositories.Authentication;

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
        Mock<IExternalStorageService> _externalStorageService;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _accessor = HttpContextAccessorFactory.Create();
            _mockRepository = new Mock<IUserDetailsRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _planMockRepository = new Mock<IPlanRepository>();
            _externalStorageService = new Mock<IExternalStorageService>();
            _mockUserManager = UserManagerFactory.CreateMock();

            _service = new UserService(_mockUserRepository.Object, _mockRepository.Object, _planMockRepository.Object, _mapper, _accessor, _mockUserManager.Object, _externalStorageService.Object);
        }

        [TestMethod]
        public void AssociatePlan()
        {
            var userSeed = UserSeed.GetUserDetails();
            var planSeed = PlanSeed.GetPlans().FirstOrDefault();
            var mockRequest = Guid.NewGuid();

            // Arrange
            _mockRepository.Setup(r => r.Update(It.IsAny<UserDetails>()));
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
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()).Result).Returns(UserSeed.GetUsers());

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
            var authSeed = UserSeed.GetUsers();
            var mockRequest = Mock.Of<UserImageDto>();
            var SaveUrl = "./Content/Image/Teste.jpg";

            // Arrange
            _externalStorageService.Setup(x => x.UploadFileAsync(mockRequest).Result).Returns(SaveUrl);
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()).Result).Returns(UserSeed.GetUsers());

            // Act
            var result = _service.AddUserImage(mockRequest);

            // Assert
            _externalStorageService.Verify(a => a.UploadFileAsync(mockRequest), Times.Once);          
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

    }
}
