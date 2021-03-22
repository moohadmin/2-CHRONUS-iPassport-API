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
        Mock<ICityRepository> _mockCityRepository;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IHealthUnitRepository>();
            _mockAddressRepository = new Mock<IAddressRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _mockCityRepository = new Mock<ICityRepository>();

            _service = new HealthUnitService(_mockRepository.Object, _mockLocalizer.Object, _mapper, _mockAddressRepository.Object, _mockCityRepository.Object);
        }

        [TestMethod]
        public void FindByNameParts()
        {
            // Arrange
            var mockRequest = Mock.Of<GetHealthUnitPagedFilter>();
            _mockRepository.Setup(x => x.GetPagedHealthUnits(It.IsAny<GetHealthUnitPagedFilter>()).Result).Returns(HealthUnitSeed.GetPaged());
            
            // Act
            var result = _service.FindByNameParts(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.GetPagedHealthUnits(It.IsAny<GetHealthUnitPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void Add()
        {
            // Arrange
            var mockRequest = Mock.Of<HealthUnitCreateDto>(x =>x.Address == Mock.Of<AddressCreateDto>() && x.TypeId == Guid.NewGuid());

            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<HealthUnit>()).Result).Returns(true);
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockAddressRepository.Setup(x => x.InsertAsync(It.IsAny<Address>()).Result).Returns(true);

            // Act
            var result = _service.Add(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.InsertAsync(It.IsAny<HealthUnit>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}
