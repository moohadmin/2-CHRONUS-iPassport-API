using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Services;
using iPassport.Domain.Repositories;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
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
        Mock<IPassportUseRepository> _mockUseRepository;
        Mock<IUserDetailsRepository> _mockUserDeatilsRepository;
        IPassportService _service;
        IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IPassportRepository>();
            _mockUserDeatilsRepository = new Mock<IUserDetailsRepository>();
            _mockUseRepository = new Mock<IPassportUseRepository>();
            _service = new PassportService(_mapper, _mockRepository.Object, _mockUserDeatilsRepository.Object, _mockUseRepository.Object);
        }

        [TestMethod]
        public void Get_MustReturnsOK()
        {
            string userId = "97d4bb42-a0cb-4a72-abaa-ded84823a166";
            var userSeed = UserSeed.GetUserDetails();
            var passportSeed = PassportSeed.Get();
            // Arrange
            _mockUserDeatilsRepository.Setup(r => r.FindWithUser(It.IsNotNull<Guid>()).Result).Returns(userSeed);
            _mockRepository.Setup(r => r.FindByUser(It.IsNotNull<Guid>()).Result).Returns(passportSeed);
            // Act
            var result = _service.Get(userId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(PassportViewModel));
        }
    }
}

