using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
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
    public class CountryServiceTest
    {
        Mock<ICountryRepository> _mockRepository;
        ICountryService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;
        IHttpContextAccessor _accessor;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<ICountryRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _accessor = HttpContextAccessorFactory.Create();

            _service = new CountryService(_mockRepository.Object, _mockLocalizer.Object, _mapper, _accessor);
        }

        [TestMethod]
        public void Add_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<CountryCreateDto>();
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Country>()).Result).Returns(true);

            // Act
            var result = _service.Add(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.InsertAsync(It.IsAny<Country>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void FindById_MustReturnOk()
        {
            // Arrange
            var mockRequest = Guid.NewGuid();
            _mockRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CountrySeed.GetCountry());

            // Act
            var result = _service.FindById(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.Find(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void FindByNameParts_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<GetByNamePartsPagedFilter>();
            _mockRepository.Setup(x => x.FindByNameParts(It.IsAny<GetByNamePartsPagedFilter>(), It.IsAny<AccessControlDTO>()).Result).Returns(CountrySeed.GetPaged());

            // Act
            var result = _service.FindByNameParts(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.FindByNameParts(It.IsAny<GetByNamePartsPagedFilter>(), It.IsAny<AccessControlDTO>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}
