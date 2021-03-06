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
    public class StateServiceTest
    {
        Mock<IStateRepository> _mockRepository;
        IStateService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IStateRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();

            _service = new StateService(_mockRepository.Object, _mockLocalizer.Object, _mapper);
        }

        [TestMethod]
        public void FindByNameParts_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<GetByIdPagedFilter>();
            _mockRepository.Setup(x => x.GetByCountryId(It.IsAny<GetByIdPagedFilter>()).Result).Returns(StateSeed.GetPaged());

            // Act
            var result = _service.GetByCountryId(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.GetByCountryId(It.IsAny<GetByIdPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.AreEqual(true, result.Result.Success);
        }

        [TestMethod]
        public void Add_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<StateCreateDto>();
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<State>()).Result).Returns(true);

            // Act
            var result = _service.Add(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.InsertAsync(It.IsAny<State>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.AreEqual(true,result.Result.Success);
        }
    }
}
