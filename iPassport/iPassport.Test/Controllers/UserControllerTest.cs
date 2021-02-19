using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Application.Interfaces;
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
        IMapper _mapper;
        UserController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IUserService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new UserController(_mapper, _mockService.Object);
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
    }
}
