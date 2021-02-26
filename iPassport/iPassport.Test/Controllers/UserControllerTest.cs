using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Application.Interfaces;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
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
        IMapper _mapper;
        UserController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IUserService>();
            _mockVaccineService = new Mock<IUserVaccineService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new UserController(_mapper, _mockService.Object, _mockVaccineService.Object);
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
            var mockRequest = Mock.Of<PageFilterRequest>();

            // Arrange
            _mockVaccineService.Setup(r => r.GetUserVaccines(It.IsAny<PageFilter>()).Result);

            // Act
            var result = _controller.GetPagedUserVaccines(mockRequest);

            // Assert
            _mockVaccineService.Verify(a => a.GetUserVaccines(It.IsAny<PageFilter>()), Times.Once);
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
    }
}
