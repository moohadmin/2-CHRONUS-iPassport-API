using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Services;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class VaccineServiceTest
    {
        Mock<IUserVaccineRepository> _mockRepository;
        IUserVaccineService _service;
        IHttpContextAccessor _accessor;
        IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _accessor = HttpContextAccessorFactory.Create();
            _mockRepository = new Mock<IUserVaccineRepository>();
            _service = new UserVaccineService(_mapper, _mockRepository.Object, _accessor);
        }

        [TestMethod]
        public void GetAll()
        {
            var seed = VaccineSeed.GetPagedVaccines();
            var mockFilter = Mock.Of<PageFilter>();
            // Arrange
            _mockRepository.Setup(r => r.GetPagedUserVaccines(It.IsAny<Guid>(), It.IsAny<PageFilter>()).Result).Returns(seed);

            // Act
            var result = _service.GetUserVaccines(mockFilter);

            // Assert
            _mockRepository.Verify(a => a.GetPagedUserVaccines(It.IsAny<Guid>(), It.IsAny<PageFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(IList<UserVaccineViewModel>));
        }
    }
}
