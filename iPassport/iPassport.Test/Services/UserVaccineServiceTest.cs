using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Http;
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
        IUserVaccineService _service;
        IHttpContextAccessor _accessor;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _accessor = HttpContextAccessorFactory.Create();
            _mockRepository = new Mock<IUserVaccineRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();

            _service = new UserVaccineService(_mapper, _mockRepository.Object, _accessor, _mockLocalizer.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            var seed = UserVaccineSeed.GetPagedUserVaccines();
            var mockFilter = Mock.Of<PageFilter>();
            // Arrange
            _mockRepository.Setup(r => r.GetPagedUserVaccines(It.IsAny<Guid>(), It.IsAny<PageFilter>()).Result).Returns(seed);

            // Act
            var result = _service.GetUserVaccines(mockFilter);

            // Assert
            _mockRepository.Verify(a => a.GetPagedUserVaccines(It.IsAny<Guid>(), It.IsAny<PageFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}
