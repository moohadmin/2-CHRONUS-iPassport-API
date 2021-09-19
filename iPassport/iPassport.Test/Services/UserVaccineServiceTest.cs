using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class UserVaccineServiceTest
    {
        Mock<IUserVaccineRepository> _mockRepository;
        Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        Mock<IUserRepository> _mockUserRepository;
        IUserVaccineService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IUserVaccineRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            
            var accessor = HttpContextAccessorFactory.Create();

            _service = new UserVaccineService(_mapper, _mockRepository.Object, _mockUserDetailsRepository.Object, _mockUserRepository.Object, _mockLocalizer.Object, accessor);
        }

        [TestMethod]
        public void GetPagedUserVaccines()
        {
            var seed = UserVaccineSeed.GetPagedUserVaccines();
            var userSeed = UserSeed.GetUserDetails();

            var mockFilter = Mock.Of<GetByIdPagedFilter>();
            
            // Arrange
            _mockRepository.Setup(r => r.GetPagedUserVaccinesByPassportId(It.IsAny<GetByIdPagedFilter>(), It.IsAny<DateTime>()).Result).Returns(seed);
            _mockUserDetailsRepository.Setup(r => r.GetLoadedUserById(It.IsAny<Guid>()).Result).Returns(userSeed);
            _mockUserDetailsRepository.Setup(r => r.GetByPassportId(It.IsAny<Guid>()).Result).Returns(userSeed);
            _mockUserRepository.Setup(r => r.GetUserBirthdayDate(It.IsAny<Guid>()).Result).Returns(DateTime.Now);
            // Act
            var result = _service.GetUserVaccines(mockFilter);

            // Assert
            _mockRepository.Verify(a => a.GetPagedUserVaccinesByPassportId(It.IsAny<GetByIdPagedFilter>(), It.IsAny<DateTime>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetCurrentUserVaccines()
        {
            var seed = UserVaccineSeed.GetPagedUserVaccines();
            var userSeed = UserSeed.GetUserDetails();

            var mockFilter = Mock.Of<PageFilter>();

            // Arrange
            _mockRepository.Setup(r => r.GetPagedUserVaccinesByUserId(It.IsAny<GetByIdPagedFilter>(), It.IsAny<DateTime>()).Result).Returns(seed);
            _mockUserDetailsRepository.Setup(r => r.GetLoadedUserById(It.IsAny<Guid>()).Result).Returns(userSeed);
            _mockUserRepository.Setup(r => r.GetUserBirthdayDate(It.IsAny<Guid>()).Result).Returns(DateTime.Now);

            // Act
            var result = _service.GetCurrentUserVaccines(mockFilter);

            // Assert
            _mockRepository.Verify(a => a.GetPagedUserVaccinesByUserId(It.IsAny<GetByIdPagedFilter>(), It.IsAny<DateTime>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}
