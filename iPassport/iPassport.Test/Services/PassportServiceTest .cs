using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class PassportServiceTest
    {
        Mock<IPassportRepository> _mockRepository;
        Mock<IPassportDetailsRepository> _mockRepositoryPassportDetails;
        Mock<IPassportUseRepository> _mockUseRepository;
        Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        IHttpContextAccessor _accessor;
        IPassportService _service;
        IMapper _mapper;
        PassportUseCreateDto _accessDto;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IPassportRepository>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockUseRepository = new Mock<IPassportUseRepository>();
            _accessor = HttpContextAccessorFactory.Create();
            _mockRepositoryPassportDetails = new Mock<IPassportDetailsRepository>();

            _service = new PassportService(_mapper, _mockRepository.Object, _mockUserDetailsRepository.Object, _mockUseRepository.Object, _accessor, _mockRepositoryPassportDetails.Object);

            _accessDto = new PassportUseCreateDto()
            {
                PassportDetailsId = Guid.Parse("3d7d5b92-0ec3-465c-9342-e2c37ad7f5b0"),
                Latitude = "10.10",
                Longitude = "11.50"
            };
        }

        [TestMethod]
        public void Get_MustReturnsOK()
        {
            var userSeed = UserSeed.GetUserDetails();
            var passportSeed = PassportSeed.Get();
            // Arrange
            _mockUserDetailsRepository.Setup(r => r.FindWithUser(It.IsNotNull<Guid>()).Result).Returns(userSeed);
            _mockRepository.Setup(r => r.FindByUser(It.IsNotNull<Guid>()).Result).Returns(passportSeed);
            // Act
            var result = _service.Get();

            //Assert
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(PassportViewModel));
        }

        [TestMethod]
        public void AddAccessApproved_MustReturnsOK()
        {
            var userSeed = UserSeed.GetUserDetails();
            var passportSeed = PassportSeed.Get();
            // Arrange
            _mockUserDetailsRepository.Setup(r => r.FindWithUser(It.IsNotNull<Guid>()).Result).Returns(userSeed);
            _mockRepository.Setup(r => r.FindByPassportDetailsValid(It.IsNotNull<Guid>()).Result).Returns(passportSeed);
            _mockUseRepository.Setup(r => r.InsertAsync(It.IsAny<PassportUse>()));
            // Act
            var result = _service.AddAccessApproved(_accessDto);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.AreEqual("Acesso Aprovado", result.Result.Message);

        }

        [TestMethod]
        public void AddAccessDenied_MustReturnsOK()
        {
            var userSeed = UserSeed.GetUserDetails();
            var passportSeed = PassportSeed.Get();
            // Arrange
            _mockUserDetailsRepository.Setup(r => r.FindWithUser(It.IsNotNull<Guid>()).Result).Returns(userSeed);
            _mockRepository.Setup(r => r.FindByPassportDetailsValid(It.IsNotNull<Guid>()).Result).Returns(passportSeed);
            _mockUseRepository.Setup(r => r.InsertAsync(It.IsAny<PassportUse>()));
            // Act
            var result = _service.AddAccessDenied(_accessDto);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.AreEqual("Acesso Recusado", result.Result.Message);
        }
    }
}

