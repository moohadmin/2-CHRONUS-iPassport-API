using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Services;
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
        Mock<IPassportUseRepository> _mockUseRepository;
        Mock<IUserDetailsRepository> _mockUserDeatilsRepository;
        IHttpContextAccessor _accessor;
        IPassportService _service;
        IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IPassportRepository>();
            _mockUserDeatilsRepository = new Mock<IUserDetailsRepository>();
            _mockUseRepository = new Mock<IPassportUseRepository>();
            _accessor = HttpContextAccessorFactory.Create();

            _service = new PassportService(_mapper, _mockRepository.Object, _mockUserDeatilsRepository.Object, _mockUseRepository.Object, _accessor);
        }

        [TestMethod]
        public void Get_MustReturnsOK()
        {
            var userSeed = UserSeed.GetUserDetails();
            var passportSeed = PassportSeed.Get();
            // Arrange
            _mockUserDeatilsRepository.Setup(r => r.FindWithUser(It.IsNotNull<Guid>()).Result).Returns(userSeed);
            _mockRepository.Setup(r => r.FindByUser(It.IsNotNull<Guid>()).Result).Returns(passportSeed);
            // Act
            var result = _service.Get();

            //Assert
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(PassportViewModel));
        }
    }
}

