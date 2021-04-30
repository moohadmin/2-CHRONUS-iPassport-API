using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces.Authentication;
using iPassport.Application.Models;
using iPassport.Application.Resources;
using iPassport.Application.Services.AuthenticationServices;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        IStringLocalizer<Resource> _localizer;
        Mock<IAddressRepository> _mockAddressRepository;
        Resource _resource;

        [TestInitialize]
        public void Setup()
        {
            _resource = ResourceFactory.Create();
            _accessor = HttpContextAccessorFactory.Create();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserManager = UserManagerFactory.CreateMock();
            _mockTokenService = new Mock<ITokenService>();
            _mockAuth2FactService = new Mock<IAuth2FactService>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _localizer = ResourceFactory.GetStringLocalizer();
            _mockAddressRepository = new Mock<IAddressRepository>();
            _service = new AccountService( _mockTokenService.Object, _mockUserManager.Object, _mockUserRepository.Object, _mockAuth2FactService.Object, _accessor, _localizer, _mockUserDetailsRepository.Object, _mockAddressRepository.Object);
        }


        [TestMethod]
        public void BasicLogin_MustReturnOK()
        {
            var username = "teste";
            var Password = "test";
            var token = "6546548955123ugyfgyuyggyu446654654";
            var hasPlan = false;
            var user = UserSeed.GetUserAgent();
            
            // Arrange
            _mockTokenService.Setup(x => x.GenerateBasic(It.IsAny<Users>(), hasPlan, It.IsAny<string>()).Result).Returns(token);
            _mockUserRepository.Setup(x => x.Update(It.IsAny<Users>()));
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<Users>(), It.IsAny<string>()).Result).Returns(true);
            _mockUserRepository.Setup(x => x.GetByUsername(It.IsAny<string>()).Result).Returns(user);
            
            // Act
            var result = _service.BasicLogin(username,Password);

            // Assert
            _mockTokenService.Verify(a => a.GenerateBasic(It.IsAny<Users>(), hasPlan, It.IsAny<string>()), Times.Once);
            _mockUserRepository.Verify(a => a.Update(It.IsAny<Users>()), Times.Once);
            _mockUserManager.Verify(a => a.CheckPasswordAsync(It.IsAny<Users>(), It.IsAny<string>()), Times.Once);
            _mockUserRepository.Verify(a => a.GetByUsername(It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        [DataRow(EUserType.Citizen)]
        [DataRow(EUserType.Admin)]
        public void BasicLogin_MustHaveAgentAcess(EUserType userType)
        {
            var username = "teste";
            var Password = "test";
            var token = "6546548955123ugyfgyuyggyu446654654";
            var hasPlan = false;
            var user = UserSeed.Get(userType);

            // Arrange
            _mockTokenService.Setup(x => x.GenerateBasic(It.IsAny<Users>(), hasPlan, It.IsAny<string>()).Result).Returns(token);
            _mockUserRepository.Setup(x => x.Update(It.IsAny<Users>()));
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<Users>(), It.IsAny<string>()).Result).Returns(true);
            _mockUserRepository.Setup(x => x.GetByUsername(It.IsAny<string>()).Result).Returns(user);

            // Assert
            Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.BasicLogin(username, Password),
                _resource.GetMessage("UserNotHaveAgentAccess"));

        }

        [TestMethod]
        public void EmailLogin_MustReturnOK()
        {
            var email = "teste";
            var Password = "test";
            var token = "6546548955123ugyfgyuyggyu446654654";

            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result).Returns(UserSeed.GetUserAdmin());
            _mockUserDetailsRepository.Setup(x => x.GetWithHealtUnityById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<Users>(), It.IsAny<string>()).Result).Returns(true);
            _mockTokenService.Setup(x => x.GenerateByEmail(It.IsAny<Users>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()).Result).Returns(token);
            _mockAddressRepository.Setup(x => x.FindFullAddress(It.IsAny<Guid>()).Result).Returns(new Address());

            // Act
            var result = _service.EmailLogin(email, Password);

            // Assert
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
            _mockUserManager.Verify(x => x.CheckPasswordAsync(It.IsAny<Users>(), It.IsAny<string>()));
            _mockTokenService.Verify(x => x.GenerateByEmail(It.IsAny<Users>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mockUserDetailsRepository.Verify(x => x.GetWithHealtUnityById(It.IsAny<Guid>()));
            _mockAddressRepository.Verify(x => x.FindFullAddress(It.IsAny<Guid>()));

            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void EmailLogin_User_MustFindUser()
        {
            var email = "teste";
            var Password = "test";
            
            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EmailLogin(email, Password)).Result;
            Assert.AreEqual(_localizer["UserOrPasswordInvalid"], ex.Message);
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
        }

        [TestMethod]
        [DataRow(EUserType.Citizen)]
        [DataRow(EUserType.Agent)]
        public void EmailLogin_MustHaveAdminAcess(EUserType userType)
        {
            var email = "teste";
            var Password = "test";

            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result).Returns(UserSeed.Get(userType));

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EmailLogin(email, Password)).Result;
            Assert.AreEqual(_localizer["UserNotHaveAdminAccess"], ex.Message);
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
        }

        [TestMethod]
        public void EmailLogin_User_MustBeActiveUser()
        {
            var email = "teste";
            var Password = "test";
            var user = UserSeed.GetUserAdmin();
            //TODO Keep only user.Deactivate() after refactory admin add
            user.Deactivate(Guid.NewGuid());
            user.UserUserTypes.FirstOrDefault().Deactivate(Guid.NewGuid());

            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result).Returns(user);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EmailLogin(email, Password)).Result;
            Assert.AreEqual(_localizer["InactiveUser"], ex.Message);
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
        }

        [TestMethod]
        public void EmailLogin_User_MustHaveProfile()
        {
            var email = "teste";
            var Password = "test";
            var user = UserSeed.GetUserAdmin();
            user.Profile = null;
            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result).Returns(user);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EmailLogin(email, Password)).Result;
            Assert.AreEqual(_localizer["UserAccessProfileNotFound"], ex.Message);
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
        }

        [TestMethod]
        public void EmailLogin_UserDetails_MustFindUserDetails()
        {
            var email = "teste";
            var Password = "test";
            var user = UserSeed.GetUserAdmin();
            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result).Returns(user);
            _mockUserDetailsRepository.Setup(x => x.GetByUserId(It.IsAny<Guid>()).Result);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EmailLogin(email, Password)).Result;
            Assert.AreEqual(_localizer["UserNotFound"], ex.Message);
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
            _mockUserDetailsRepository.Setup(x => x.GetByUserId(It.IsAny<Guid>()));
        }

        [TestMethod]
        [DataRow(EProfileKey.business)]
        [DataRow(EProfileKey.healthUnit)]
        [DataRow(EProfileKey.government)]
        public void EmailLogin_ProfileData_CompanyMustFound(EProfileKey profile)
        {
            var email = "teste";
            var Password = "test";
            var user = UserSeed.GetUserAdmin();
            var userDetails = UserSeed.GetUserDetails();
            user.Profile = ProfileSeed.Get(profile);
            user.Company = null;
            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result).Returns(user);
            _mockUserDetailsRepository.Setup(x => x.GetWithHealtUnityById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockAddressRepository.Setup(x => x.FindFullAddress(It.IsAny<Guid>()).Result).Returns(new Address());

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EmailLogin(email, Password)).Result;
            Assert.AreEqual(_localizer["CompanyNotFound"], ex.Message);
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
            _mockUserDetailsRepository.Verify(x => x.GetWithHealtUnityById(It.IsAny<Guid>()));
            _mockAddressRepository.Verify(x => x.FindFullAddress(It.IsAny<Guid>()));
        }

        [TestMethod]
        [DataRow(EProfileKey.business)]
        [DataRow(EProfileKey.healthUnit)]
        [DataRow(EProfileKey.government)]
        public void EmailLogin_ProfileData_CompanyMustBeActive(EProfileKey profile)
        {
            var email = "teste";
            var Password = "test";
            var user = UserSeed.GetUserAdmin();
            var userDetails = UserSeed.GetUserDetails();
            user.Profile = ProfileSeed.Get(profile);
            user.Company.Deactivate(Guid.NewGuid());

            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result).Returns(user);
            _mockUserDetailsRepository.Setup(x => x.GetWithHealtUnityById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockAddressRepository.Setup(x => x.FindFullAddress(It.IsAny<Guid>()).Result).Returns(new Address());

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EmailLogin(email, Password)).Result;
            Assert.AreEqual(_localizer["InactiveUserCompany"], ex.Message);
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
            _mockUserDetailsRepository.Verify(x => x.GetWithHealtUnityById(It.IsAny<Guid>()));
            _mockAddressRepository.Verify(x => x.FindFullAddress(It.IsAny<Guid>()));
        }

        [TestMethod]
        [DataRow(EProfileKey.healthUnit)]
        [DataRow(EProfileKey.government)]
        public void EmailLogin_ProfileData_CompanyMustHaveAddress(EProfileKey profile)
        {
            var email = "teste";
            var Password = "test";
            var user = UserSeed.GetUserAdmin();
            var userDetails = UserDetails.CreateUserDetail(Mock.Of<AdminDto>(x => x.Id == Guid.NewGuid() && x.HealthUnitId == Guid.NewGuid()));
            user.Profile = ProfileSeed.Get(profile);
            user.Company.Address = null;
            userDetails.HealthUnit = new HealthUnit(Guid.NewGuid(), Guid.NewGuid());

            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result).Returns(user);
            _mockUserDetailsRepository.Setup(x => x.GetWithHealtUnityById(It.IsAny<Guid>()).Result).Returns(userDetails);
            _mockAddressRepository.Setup(x => x.FindFullAddress(It.IsAny<Guid>())).Returns(Task.FromResult<Address>(null));

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EmailLogin(email, Password)).Result;
            Assert.AreEqual(_localizer["AddressNotFound"], ex.Message);
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
            //_mockUserDetailsRepository.Setup(x => x.GetByUserId(It.IsAny<Guid>()));
            _mockUserDetailsRepository.Verify(x => x.GetWithHealtUnityById(It.IsAny<Guid>()));
            _mockAddressRepository.Verify(x => x.FindFullAddress(It.IsAny<Guid>()));
        }

        [TestMethod]
        [DataRow(EProfileKey.government)]
        public void EmailLogin_ProfileData_CompanyMustHaveSegment(EProfileKey profile)
        {
            var email = "teste";
            var Password = "test";
            var user = UserSeed.GetUserAdmin();
            var userDetails = UserDetails.CreateUserDetail(Mock.Of<AdminDto>(x => x.Id == Guid.NewGuid() && x.HealthUnitId == Guid.NewGuid()));
            user.Profile = ProfileSeed.Get(profile);
            user.Company.Segment = null;
            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result).Returns(user);
            _mockUserDetailsRepository.Setup(x => x.GetWithHealtUnityById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockAddressRepository.Setup(x => x.FindFullAddress(It.IsAny<Guid>()).Result).Returns(new Address());

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EmailLogin(email, Password)).Result;
            Assert.AreEqual(_localizer["SegmentNotFound"], ex.Message);
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
            _mockUserDetailsRepository.Verify(x => x.GetWithHealtUnityById(It.IsAny<Guid>()));
            _mockAddressRepository.Verify(x => x.FindFullAddress(It.IsAny<Guid>()));
        }

        [TestMethod]
        [DataRow(EProfileKey.healthUnit)]        
        public void EmailLogin_ProfileData_MustHaveHealthUnitId(EProfileKey profile)
        {
            var email = "teste";
            var Password = "test";
            var user = UserSeed.GetUserAdmin();
            var userDetails = UserSeed.GetUserDetails();
            user.Profile = ProfileSeed.Get(profile);
            user.Company = CompanySeed.Get(null, true, ECompanySegmentType.Health);
            // Arrange
            _mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>()).Result).Returns(user);
            //_mockUserDetailsRepository.Setup(x => x.GetByUserId(It.IsAny<Guid>()).Result).Returns(userDetails);
            _mockUserDetailsRepository.Setup(x => x.GetWithHealtUnityById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUserDetails());
            _mockAddressRepository.Setup(x => x.FindFullAddress(It.IsAny<Guid>()).Result).Returns(new Address());

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.EmailLogin(email, Password)).Result;
            Assert.AreEqual(_localizer["HealthUnitNotFound"], ex.Message);
            _mockUserRepository.Verify(x => x.GetByEmail(It.IsAny<string>()));
            //_mockUserDetailsRepository.Setup(x => x.GetByUserId(It.IsAny<Guid>()));
            _mockUserDetailsRepository.Verify(x => x.GetWithHealtUnityById(It.IsAny<Guid>()));
            _mockAddressRepository.Verify(x => x.FindFullAddress(It.IsAny<Guid>()));
        }

        [TestMethod]
        public void PinLogin_MustReturnOK()
        {
            int pin = 1111;
            var userId = Guid.NewGuid();
            var acceptTerms = true;
            var token = "6546548955123ugyfgyuyggyu446654654";
            var userDetail = UserSeed.GetUserDetails();
            var hasPlan = false;
            var user = UserSeed.GetUserCitizen();
            // Arrange
            _mockAuth2FactService.Setup(x => x.ValidPin(Guid.NewGuid(), It.IsAny<string>()).Result);
            _mockUserRepository.Setup(x => x.GetById(It.IsAny<Guid>()).Result).Returns(user);
            _mockUserRepository.Setup(x => x.Update(It.IsAny<Users>()));
            _mockTokenService.Setup(x => x.GenerateBasic(It.IsAny<Users>(), hasPlan, It.IsAny<string>()).Result).Returns(token);
            _mockUserDetailsRepository.Setup(x => x.GetByUserId(It.IsAny<Guid>()).Result).Returns(userDetail);

            // Act
            var result = _service.PinLogin(pin, userId, acceptTerms);

            // Assert
            _mockAuth2FactService.Verify(x => x.ValidPin(It.IsAny<Guid>(), It.IsAny<string>()));
            _mockUserRepository.Verify(x => x.GetById(It.IsAny<Guid>()));
            _mockUserRepository.Verify(x => x.Update(It.IsAny<Users>()));
            _mockTokenService.Verify(x => x.GenerateBasic(It.IsAny<Users>(), hasPlan, It.IsAny<string>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        [DataRow(EUserType.Admin)]
        [DataRow(EUserType.Agent)]
        public void PinLogin_MustHaveCitizenAcess(EUserType userType)
        {
            int pin = 1111;
            var userId = Guid.NewGuid();
            var acceptTerms = true;

            // Arrange
            _mockUserRepository.Setup(x => x.GetById(It.IsAny<Guid>()).Result).Returns(UserSeed.Get(userType));
            

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.PinLogin(pin, userId, acceptTerms)).Result;
            Assert.AreEqual(_localizer["UserNotHaveCitizenAccess"], ex.Message);
            _mockUserRepository.Verify(x => x.GetById(It.IsAny<Guid>()));
            
        }

        [TestMethod]
        public void SendPin_MustReturnOK()
        {
            var phone = "test";
            var doctype = EDocumentType.CPF;
            var doc = "0515161565456";

            // Arrange
            _mockUserRepository.Setup(x => x.GetByDocument(doctype,doc).Result).Returns(UserSeed.GetUser());
            _mockAuth2FactService.Setup(r => r.SendPin(It.IsAny<Guid>(),phone).Result).Returns(Auth2FactMobileSeed.GetAuth2FactMobile());

            // Act
            var result = _service.SendPin(phone, doctype, doc);

            // Assert
            _mockUserRepository.Verify(x => x.GetByDocument(doctype, doc));
            _mockAuth2FactService.Verify(r => r.SendPin(It.IsAny<Guid>(), phone));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void ResetPassword_MustReturnOK()
        {
            var password = "tested";
            var passwordConfirm = "tested";
            var token = "2313214568fds645sfd456fsd456fs654fs65fsd6f5";
            // Arrange
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()).Result).Returns(UserSeed.GetUser());
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
