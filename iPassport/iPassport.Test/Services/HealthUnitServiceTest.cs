using AutoMapper;
using iPassport.Application.Exceptions;
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
using iPassport.Test.Settings.Seeds;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class HealthUnitServiceTest
    {
        Mock<IHealthUnitRepository> _mockRepository;
        Mock<IHealthUnitTypeRepository> _mockHealthUnitTypeRepository;
        IHealthUnitService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;
        Mock<IAddressRepository> _mockAddressRepository;
        Mock<ICityRepository> _mockCityRepository;
        Mock<IUnitOfWork> _mockUnitOfWork;
        Mock<ICompanyRepository> _mockCompanyRepository;
        Resource _resource;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IHealthUnitRepository>();
            _mockHealthUnitTypeRepository = new Mock<IHealthUnitTypeRepository>();
            _mockAddressRepository = new Mock<IAddressRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _mockCityRepository = new Mock<ICityRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCompanyRepository = new Mock<ICompanyRepository>();

            _resource = ResourceFactory.Create();

            _service = new HealthUnitService(_mockRepository.Object, _mockHealthUnitTypeRepository.Object, _mockLocalizer.Object, _mapper, _mockAddressRepository.Object, _mockCityRepository.Object, _mockCompanyRepository.Object, _mockUnitOfWork.Object);
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
            _mockHealthUnitTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(HealthUnitTypeSeed.GetHealthUnitTypePublic());

            // Act
            var result = _service.Add(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.InsertAsync(It.IsAny<HealthUnit>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            var mockRequest = Guid.NewGuid();

            _mockRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result)
                .Returns(HealthUnitSeed.GetHealthUnits().FirstOrDefault());
            
            _mockAddressRepository.Setup(x => x.FindFullAddress(It.IsAny<Guid>()).Result)
                .Returns(AddressSeed.Get());

            // Act
            var result = _service.GetById(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.Find(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            var mockRequest = Mock.Of<HealthUnitEditDto>(x => x.Address == Mock.Of<AddressEditDto>() && x.TypeId == Guid.NewGuid());

            _mockRepository.Setup(x => x.Update(It.IsAny<HealthUnit>()).Result)
                .Returns(true);

            _mockAddressRepository.Setup(x => x.Update(It.IsAny<Address>()).Result)
                .Returns(true);

            _mockRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result)
                .Returns(HealthUnitSeed.GetHealthUnits().FirstOrDefault());

            _mockAddressRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result)
                .Returns(AddressSeed.Get());
            
            _mockHealthUnitTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(HealthUnitTypeSeed.GetHealthUnitTypePublic());

            // Act
            var result = _service.Edit(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.Update(It.IsAny<HealthUnit>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void AddMustThrowsCityNotFound()
        {
            // Arrange
            var mockRequest = Mock.Of<HealthUnitCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>() && x.TypeId == Guid.NewGuid());

            // Act
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result);

            var message = _resource.GetMessage("CityNotFound");

            // Assert
            Assert.ThrowsExceptionAsync<BusinessException>(async() => await _service.Add(mockRequest), message);
        }

        [TestMethod]
        public void AddMustThrowsHealthUnitTypeNotFound()
        {
            // Arrange
            var mockRequest = Mock.Of<HealthUnitCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>() && x.TypeId == Guid.NewGuid());

            // Act
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockHealthUnitTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result);

            var message = _resource.GetMessage("HealthUnitTypeNotFound");

            // Assert
            Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest), message);
        }

        [TestMethod]
        public void AddMustThrowsIneRequired()
        {
            // Arrange
            var mockRequest = Mock.Of<HealthUnitCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>() 
                && x.TypeId == Guid.NewGuid() && x.Ine == null);

            // Act
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.GetByCnpj(It.IsAny<string>()).Result).Returns(HealthUnitSeed.GetHealthUnits().FirstOrDefault());
            _mockHealthUnitTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(HealthUnitTypeSeed.GetHealthUnitTypePublic());

            var message = _resource.GetMessage("IneRequired");

            // Assert
            Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest), message);
        }

        [TestMethod]
        public void AddMustThrowsCnpjRequired()
        {
            // Arrange
            var mockRequest = Mock.Of<HealthUnitCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                && x.TypeId == Guid.NewGuid() && x.Cnpj == null);

            // Act
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockHealthUnitTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(HealthUnitTypeSeed.GetHealthUnitTypePrivate());

            var message = string.Format(_resource.GetMessage("RequiredField"), "CNPJ");
            
            // Assert
            Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest), message);
        }

        [TestMethod]
        public void AddMustThrowsCnpjAlreadyRegistered()
        {
            // Arrange
            var mockRequest = Mock.Of<HealthUnitCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                && x.TypeId == Guid.NewGuid() && x.Cnpj == "10992673000129");

            // Act
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.GetByCnpj(It.IsAny<string>()).Result).Returns(HealthUnitSeed.GetHealthUnits().FirstOrDefault());
            _mockHealthUnitTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(HealthUnitTypeSeed.GetHealthUnitTypePrivate());

            var message = string.Format(_resource.GetMessage("DataAlreadyRegistered"), "CNPJ");

            // Assert
            Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest), message);
        }
    }
}
