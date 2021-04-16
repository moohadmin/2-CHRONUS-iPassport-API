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
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class CityServiceTest
    {
        Mock<ICityRepository> _mockRepository;
        Mock<IStateRepository> _mockStateRepository;
        ICityService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<ICityRepository>();
            _mockStateRepository = new Mock<IStateRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();

            _service = new CityService(_mockRepository.Object, _mockLocalizer.Object, _mapper, _mockStateRepository.Object, null);
        }

        [TestMethod]
        public void FindByStateAndNameParts_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<GetByIdAndNamePartsPagedFilter>();
            _mockRepository.Setup(x => x.FindByStateAndNameParts(It.IsAny<GetByIdAndNamePartsPagedFilter>()).Result).Returns(CitySeed.GetPaged());

            // Act
            var result = _service.FindByStateAndNameParts(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.FindByStateAndNameParts(It.IsAny<GetByIdAndNamePartsPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.AreEqual(true, result.Result.Success);
        }

        [TestMethod]
        public void Add_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<CityCreateDto>();
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<City>()).Result).Returns(true);
            _mockStateRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(StateSeed.GetState());

            // Act
            var result = _service.Add(mockRequest);

            // Assert
            _mockRepository.Verify(x => x.InsertAsync(It.IsAny<City>()));
            _mockStateRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.AreEqual(true, result.Result.Success);
        }

    }
}
