using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Application.Services.AuthenticationServices;
using System.Collections.Generic;
using iPassport.Domain.Enums;
using Microsoft.Extensions.Localization;
using iPassport.Application.Resources;

namespace iPassport.Test.Services
{
    [TestClass]
    public class AccountServiceTest
    {
        Mock<IUserRepository> _mockUserRepository;
        Mock<ITokenService> _mockTokenService;
        Mock<IAuth2FactService> _mockAuth2FactService;
        IHttpContextAccessor _accessor;
        Mock<UserManager<Users>> _mockUserManager;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;
        IAccountService _service;

        [TestInitialize]
        public void Setup()
        {
            _accessor = HttpContextAccessorFactory.Create();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserManager = UserManagerFactory.CreateMock();
            _mockTokenService = new Mock<ITokenService>();
            _mockAuth2FactService = new Mock<IAuth2FactService>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();

            _service = new AccountService( _mockTokenService.Object, _mockUserManager.Object, _mockUserRepository.Object, _mockAuth2FactService.Object, _accessor, _mockLocalizer.Object);
        }

        [TestMethod]
        public void BasicLogin_MustReturnOK()
        {
            var user = "teste";
            var Password = "test";
            var token = "6546548955123ugyfgyuyggyu446654654";

            // Arrange
            _mockTokenService.Setup(x => x.GenerateBasic(It.IsAny<Users>())).Returns(token);
            _mockUserRepository.Setup(x => x.Update(It.IsAny<Users>()));
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<Users>(), It.IsAny<string>()).Result).Returns(true);
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()).Result).Returns(UserSeed.GetUsers());
            
            // Act
            var result = _service.BasicLogin(user,Password);

            // Assert
            _mockTokenService.Verify(a => a.GenerateBasic(It.IsAny<Users>()), Times.Once);
            _mockUserRepository.Verify(a => a.Update(It.IsAny<Users>()), Times.Once);
            _mockUserManager.Verify(a => a.CheckPasswordAsync(It.IsAny<Users>(), It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(a => a.FindByNameAsync(It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void EmailLogin_MustReturnOK()
        {
            var email = "teste";
            var Password = "test";
            var token = "6546548955123ugyfgyuyggyu446654654";
            var userSeed = UserSeed.GetUserDetails();
            var Roles = new List<string>() { "Role1", "Roles2" };
            // Arrange
            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()).Result).Returns(UserSeed.GetUsers());
            _mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<Users>()).Result).Returns(Roles);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<Users>(), It.IsAny<string>()).Result).Returns(true);
            _mockTokenService.Setup(x => x.GenerateByEmail(It.IsAny<Users>(), It.IsAny<string>())).Returns(token);
            _mockUserRepository.Setup(x => x.Update(It.IsAny<Users>()));
            
            
            // Act
            var result = _service.EmailLogin(email, Password);

            // Assert
            _mockUserManager.Verify(x => x.FindByEmailAsync(It.IsAny<string>()));
            _mockUserManager.Verify(x => x.GetRolesAsync(It.IsAny<Users>()));
            _mockUserManager.Verify(x => x.CheckPasswordAsync(It.IsAny<Users>(), It.IsAny<string>()));
            _mockTokenService.Verify(x => x.GenerateByEmail(It.IsAny<Users>(), It.IsAny<string>()));
            _mockUserRepository.Verify(x => x.Update(It.IsAny<Users>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void MobileLogin_MustReturnOK()
        {
            int pin = 1111;
            var userId = Guid.NewGuid();
            var acceptTerms = true;
            var token = "6546548955123ugyfgyuyggyu446654654";
            var userSeed = UserSeed.GetUserDetails();
            // Arrange
            _mockAuth2FactService.Setup(x => x.ValidPin(Guid.NewGuid(), It.IsAny<string>()).Result);
            _mockUserRepository.Setup(x => x.FindById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUsers());
            _mockUserRepository.Setup(x => x.Update(It.IsAny<Users>()));
            _mockTokenService.Setup(x => x.GenerateBasic(It.IsAny<Users>())).Returns(token);

            // Act
            var result = _service.MobileLogin(pin, userId, acceptTerms);

            // Assert
            _mockAuth2FactService.Verify(x => x.ValidPin(It.IsAny<Guid>(), It.IsAny<string>()));
            _mockUserRepository.Verify(x => x.FindById(It.IsAny<Guid>()));
            _mockUserRepository.Verify(x => x.Update(It.IsAny<Users>()));
            _mockTokenService.Verify(x => x.GenerateBasic(It.IsAny<Users>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void SendPin_MustReturnOK()
        {
            var phone = "test";
            var doctype = EDocumentType.CPF;
            var doc = "0515161565456";

            // Arrange
            _mockUserRepository.Setup(x => x.FindByDocument(doctype,doc).Result).Returns(UserSeed.GetUsers());
            _mockAuth2FactService.Setup(r => r.SendPin(It.IsAny<Guid>(),phone).Result).Returns(Auth2FactMobileSeed.GetAuth2FactMobile());

            // Act
            var result = _service.SendPin(phone, doctype, doc);

            // Assert
            _mockUserRepository.Verify(x => x.FindByDocument(doctype, doc));
            _mockAuth2FactService.Verify(r => r.SendPin(It.IsAny<Guid>(), phone));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void ResendPin_MustReturnOK()
        {
            var phone = "test";
            var userId = Guid.NewGuid();

            // Arrange
            _mockUserRepository.Setup(x => x.FindById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUsers());
            _mockAuth2FactService.Setup(r => r.ResendPin(It.IsAny<Guid>(), phone).Result).Returns(Auth2FactMobileSeed.GetAuth2FactMobile());

            // Act
            var result = _service.ResendPin(phone, userId);

            // Assert
            _mockUserRepository.Verify(x => x.FindById(It.IsAny<Guid>()));
            _mockAuth2FactService.Verify(r => r.ResendPin(It.IsAny<Guid>(), phone));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
        }

        [TestMethod]
        public void ResetPassword_MustReturnOK()
        {
            var password = "tested";
            var passwordConfirm = "tested";
            var token = "2313214568fds645sfd456fsd456fs654fs65fsd6f5";
            // Arrange
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()).Result).Returns(UserSeed.GetUsers());
            _mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<Users>()).Result).Returns(token);
            _mockUserManager.Setup(x => x.ResetPasswordAsync(It.IsAny<Users>(),token,passwordConfirm).Result).Returns(IdentityResult.Success);

            // Act
            var result = _service.ResetPassword(password, passwordConfirm);

            // Assert
            _mockUserManager.Verify(x => x.FindByIdAsync(It.IsAny<string>()));
            _mockUserManager.Verify(x => x.GeneratePasswordResetTokenAsync(It.IsAny<Users>()));
            _mockUserManager.Verify(x => x.ResetPasswordAsync(It.IsAny<Users>(), token, passwordConfirm));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
        }
    }
}
