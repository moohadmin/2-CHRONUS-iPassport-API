using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.PassportIdentityContext;
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
    public class HealthUnitServiceTest
    {
        Mock<IHealthUnitRepository> _mockRepository;
        IHealthUnitService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;
        Mock<IAddressRepository> _mockAddressRepository;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IHealthUnitRepository>();
            _mockAddressRepository = new Mock<IAddressRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();

            _service = new HealthUnitService(_mockRepository.Object, _mockLocalizer.Object, _mapper, _mockAddressRepository.Object);
        }

        [TestMethod]
        public void FindByNameParts_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<GetByNamePartsPagedFilter>();
            _mockRepository.Setup(x => x.FindByNameParts(It.IsAny<GetByNamePartsPagedFilter>()).Result).Returns(HealthUnitSeed.GetPaged());
            // Act
            var result = _service.FindByNameParts(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.FindByNameParts(It.IsAny<GetByNamePartsPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

    }
}
