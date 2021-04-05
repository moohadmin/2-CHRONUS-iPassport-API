using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace iPassport.Test.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        Mock<IUserService> _mockService;
        Mock<IUserVaccineService> _mockVaccineService;
        Mock<IUserDiseaseTestService> _mockUserDiseaseTestService;
        IMapper _mapper;
        UserController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IUserService>();
            _mockVaccineService = new Mock<IUserVaccineService>();
            _mockUserDiseaseTestService = new Mock<IUserDiseaseTestService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new UserController(_mapper, _mockService.Object, _mockVaccineService.Object, _mockUserDiseaseTestService.Object);
        }

        [TestMethod]
        public void PutUserPlan_MustReturnOk()
        {
            var mockRequest = Guid.NewGuid();

            // Arrange
            _mockService.Setup(r => r.AssociatePlan(It.IsAny<Guid>()));

            // Act
            var result = _controller.PutUserPlan(mockRequest);

            // Assert
            _mockService.Verify(a => a.AssociatePlan(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void PostUserPlan_MustReturnOk()
        {
            var mockRequest = Guid.NewGuid();

            // Arrange
            _mockService.Setup(r => r.AssociatePlan(It.IsAny<Guid>()));

            // Act
            var result = _controller.PostUserPlan(mockRequest);

            // Assert
            _mockService.Verify(a => a.AssociatePlan(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetCurrentUser_MustReturnOk()
        {
            // Arrange
            _mockService.Setup(r => r.GetCurrentUser());

            // Act
            var result = _controller.GetCurrentUser();

            // Assert
            _mockService.Verify(a => a.GetCurrentUser(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void UserImageUpdload_MustReturnOk()
        {
            var mockRequest = new UserImageRequest();

            // Arrange
            _mockService.Setup(r => r.AddUserImage(It.IsAny<UserImageDto>()));

            // Act
            var result = _controller.UserImageUpload(mockRequest);

            // Assert
            _mockService.Verify(a => a.AddUserImage(It.IsAny<UserImageDto>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetUserPlan_MustReturnOk()
        {
            // Arrange
            _mockService.Setup(r => r.GetUserPlan());

            // Act
            var result = _controller.GetUserPlan();

            // Assert
            _mockService.Verify(a => a.GetUserPlan(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetPagedUserVaccines_MustReturnOk()
        {
            var mockRequest = Mock.Of<GetPagedUserVaccinesByPassportRequest>();

            // Arrange
            _mockVaccineService.Setup(r => r.GetUserVaccines(It.IsAny<GetByIdPagedFilter>()).Result);

            // Act
            var result = _controller.GetPagedUserVaccines(mockRequest);

            // Assert
            _mockVaccineService.Verify(a => a.GetUserVaccines(It.IsAny<GetByIdPagedFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetPagedCurrentUserVaccines_MustReturnOk()
        {
            var mockRequest = Mock.Of<PageFilterRequest>();

            // Arrange
            _mockVaccineService.Setup(r => r.GetCurrentUserVaccines(It.IsAny<PageFilter>()).Result);

            // Act
            var result = _controller.GetPagedUserVaccines(mockRequest);

            // Assert
            _mockVaccineService.Verify(a => a.GetCurrentUserVaccines(It.IsAny<PageFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetLoggedCitzenCount_MustReturnOk()
        {
            var seed = new Random().Next(99999);

            // Arrange
            _mockService.Setup(r => r.GetLoggedCitzenCount().Result).Returns(new ResponseApi(true, "Test Success!", seed));

            // Act
            var result = _controller.GetLoggedCitzenCount();

            // Assert
            _mockService.Verify(a => a.GetLoggedCitzenCount(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetRegisteredUsersCount_MustReturnOk()
        {
            // Arrange
             var mockRequest = Mock.Of<GetRegisteredUsersCountRequest>();
            _mockService.Setup(r => r.GetRegisteredUserCount(It.IsAny<GetRegisteredUserCountFilter>()));

            // Act
            var result = _controller.GetRegisteredUsersCount(mockRequest);

            // Assert
            _mockService.Verify(a => a.GetRegisteredUserCount(It.IsAny<GetRegisteredUserCountFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetLoggedAgentCount_MustReturnOk()
        {
            var seed = new Random().Next(99999);

            // Arrange
            _mockService.Setup(r => r.GetLoggedAgentCount().Result).Returns(new ResponseApi(true, "Test Success!", seed));

            // Act
            var result = _controller.GetLoggedAgentCount();

            // Assert
            _mockService.Verify(a => a.GetLoggedAgentCount(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void AddAgent_MustReturnOk()
        {
            var mockrequest = Mock.Of<UserAgentCreateRequest>();

            // Arrange
            _mockService.Setup(r => r.AddAgent(It.IsAny<UserAgentCreateDto>()));

            // Act
            var result = _controller.AddAgent(mockrequest);

            // Assert
            _mockService.Verify(r => r.AddAgent(It.IsAny<UserAgentCreateDto>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetByNameParts_MustReturnOk()
        {
            var mockrequest = Mock.Of<GetCitzenPagedRequest>();

            // Arrange
            _mockService.Setup(r => r.GetPaggedCizten(It.IsAny<GetCitzenPagedFilter>()));

            // Act
            var result = _controller.GetCitizenByNameParts(mockrequest);

            // Assert
            _mockService.Verify(r => r.GetPaggedCizten(It.IsAny<GetCitzenPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetPagedUserTests_MustReturnOk()
        {
            var mockRequest = Mock.Of<GetPagedUserVaccinesByPassportRequest>();

            // Arrange
            _mockUserDiseaseTestService.Setup(r => r.GetUserDiseaseTest(It.IsAny<GetByIdPagedFilter>()).Result);

            // Act
            var result = _controller.GetPagedUserTests(mockRequest);

            // Assert
            _mockUserDiseaseTestService.Verify(a => a.GetUserDiseaseTest(It.IsAny<GetByIdPagedFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetCurrentUserDiseaseTest_MustReturnOk()
        {
            var mockRequest = Mock.Of<PageFilterRequest>();

            // Arrange
            _mockUserDiseaseTestService.Setup(r => r.GetCurrentUserDiseaseTest(It.IsAny<PageFilter>()).Result);

            // Act
            var result = _controller.GetPagedUserTests(mockRequest);

            // Assert
            _mockUserDiseaseTestService.Verify(a => a.GetCurrentUserDiseaseTest(It.IsAny<PageFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetCitizenById_MustReturnOk()
        {
            var mockRequest = Guid.NewGuid();

            // Arrange
            _mockService.Setup(r => r.GetCitizenById(It.IsAny<Guid>()));

            // Act
            var result = _controller.GetCitizenById(mockRequest);

            // Assert
            _mockService.Verify(r => r.GetCitizenById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void EditCitizen_MustReturnOk()
        {
            var mockRequest = Mock.Of<CitizenEditRequest>();

            // Arrange
            _mockService.Setup(r => r.EditCitizen(It.IsAny<CitizenEditDto>()));

            // Act
            var result = _controller.EditCitizen(mockRequest);

            // Assert
            _mockService.Verify(r => r.EditCitizen(It.IsAny<CitizenEditDto>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void AddAdmin_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<AdminCreateRequest>();
            _mockService.Setup(r => r.AddAdmin(It.IsAny<AdminCreateDto>()));

            // Act
            var result = _controller.AddAdmin(mockRequest);

            // Assert
            _mockService.Verify(r => r.AddAdmin(It.IsAny<AdminCreateDto>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            
        }

        [TestMethod]
        public void GetAdminById_MustReturnOk()
        {
            var mockRequest = Guid.NewGuid();

            // Arrange
            _mockService.Setup(r => r.GetAdminById(It.IsAny<Guid>()).Result)
                .Returns(new ResponseApi(true, "test", UserSeed.GetAdminDetails()));

            // Act
            var result = _controller.GetAdminById(mockRequest);

            // Assert
            _mockService.Verify(r => r.GetAdminById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void PaggedUserAdmin_MustReturnOk()
        {
            var mockrequest = Mock.Of<GetAdminUserPagedRequest>();

            // Arrange
            _mockService.Setup(r => r.GetPagedAdmins(It.IsAny<GetAdminUserPagedFilter>()).Result)
                .Returns(UserSeed.GetPagedAdmins());

            // Act
            var result = _controller.GetPagedAdmins(mockrequest);

            // Assert
            _mockService.Verify(r => r.GetPagedAdmins(It.IsAny<GetAdminUserPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

    }
}
