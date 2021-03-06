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
        ICityService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<ICityRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();

            _service = new CityService(_mockRepository.Object, _mockLocalizer.Object, _mapper);
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


    }
}
