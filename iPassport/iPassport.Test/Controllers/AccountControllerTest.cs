using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Application.Interfaces.Authentication;
using iPassport.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace iPassport.Test.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        Mock<IAccountService> _mockService;
        AccountController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IAccountService>();
            _controller = new AccountController(_mockService.Object);
        }

        [TestMethod]
        public void BasicLogin_MustReturnOk()
        {
            // Arrange
            var BasicLoginRequest = Mock.Of<BasicLoginRequest>();
            _mockService.Setup(r => r.BasicLogin(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var result = _controller.BasicLogin(BasicLoginRequest);

            // Assert
            _mockService.Verify(a => a.BasicLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void LoginByEmail_MustReturnOk()
        {
            // Arrange
            var EmailLoginRequest = Mock.Of<EmailLoginRequest>();
            _mockService.Setup(r => r.EmailLogin(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var result = _controller.LoginByEmail(EmailLoginRequest);

            // Assert
            _mockService.Verify(a => a.EmailLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void MobileLogin_MustReturnOk()
        {
            // Arrange
            var LoginMobileRequest = Mock.Of<LoginMobileRequest>();
            _mockService.Setup(r => r.MobileLogin(It.IsAny<int>(), It.IsAny<Guid>(), It.IsAny<bool>()));

            // Act
            var result = _controller.MobileLogin(LoginMobileRequest);

            // Assert
            _mockService.Verify(a => a.MobileLogin(It.IsAny<int>(), It.IsAny<Guid>(), It.IsAny<bool>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void SendPin_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<PinRequest>();
            _mockService.Setup(r => r.SendPin(It.IsAny<string>(), It.IsAny<EDocumentType>(), It.IsAny<string>()));

            // Act
            var result = _controller.SendPin(mockRequest);

            // Assert
            _mockService.Verify(a => a.SendPin(It.IsAny<string>(), It.IsAny<EDocumentType>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void PasswordReset_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<ResetPasswordRequest>();
            _mockService.Setup(r => r.ResetPassword(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var result = _controller.PasswordReset(mockRequest);

            // Assert
            _mockService.Verify(a => a.ResetPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void ResendPins_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<ResendPinRequest>();
            _mockService.Setup(r => r.ResendPin(It.IsAny<string>(), It.IsAny<Guid>()).Result);

            // Act
            var result = _controller.ResendPin(mockRequest);

            // Assert
            _mockService.Verify(a => a.ResendPin(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


    }
}
