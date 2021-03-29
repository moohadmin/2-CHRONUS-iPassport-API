using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.HealthUnit;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Test.Controllers
{
    [TestClass]
    public class HealthUnitControllerTest
    {
        Mock<IHealthUnitService> _mockService;

        IMapper _mapper;
        HealthUnitController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IHealthUnitService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new HealthUnitController(_mapper, _mockService.Object);
        }

        [TestMethod]
        public void GetByNameParts_MustReturnOk()
        {
            var mockRequest = Mock.Of<GetHealthUnitPagedRequest>();

            // Arrange
            _mockService.Setup(r => r.FindByNameParts(It.IsAny<GetHealthUnitPagedFilter>()).Result)
                .Returns(new PagedResponseApi(true, "success", 1, 1, 1, 10, HealthUnitSeed.GetHealthUnits()));

            // Act
            var result = _controller.GetByNameParts(mockRequest);

            // Assert
            _mockService.Verify(a => a.FindByNameParts(It.IsAny<GetHealthUnitPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Add_MustReturnOk()
        {
            var mockRequest = Mock.Of<HealthUnitCreateRequest>(x => x.Address == Mock.Of<AddressCreateRequest>(x => x.CityId == Guid.NewGuid()) && x.TypeId == Guid.NewGuid());

            // Arrange
            _mockService.Setup(r => r.Add(It.IsAny<HealthUnitCreateDto>()).Result)
                .Returns(new ResponseApi(true,"success", HealthUnitSeed.GetHealthUnits().FirstOrDefault()));

            // Act
            var result = _controller.Add(mockRequest);

            // Assert
            _mockService.Verify(a => a.Add(It.IsAny<HealthUnitCreateDto>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetById_MustReturnOk()
        {
            var mockRequest = Guid.NewGuid();

            // Arrange
            _mockService.Setup(r => r.GetById(It.IsAny<Guid>()).Result)
                .Returns(new ResponseApi(true, "test", HealthUnitSeed.GetHealthUnits().FirstOrDefault()));

            // Act
            var result = _controller.GetById(mockRequest);

            // Assert
            _mockService.Verify(a => a.GetById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Edit_MustReturnOk()
        {
            var mockRequest = Mock.Of<HealthUnitEditRequest>(x => x.Address == Mock.Of<AddressEditRequest>(x => x.CityId == Guid.NewGuid()) && x.TypeId == Guid.NewGuid());

            // Arrange
            _mockService.Setup(r => r.Edit(It.IsAny<HealthUnitEditDto>()).Result)
                .Returns(new ResponseApi(true, "success", HealthUnitSeed.GetHealthUnits().FirstOrDefault()));

            // Act
            var result = _controller.Edit(mockRequest);

            // Assert
            _mockService.Verify(a => a.Edit(It.IsAny<HealthUnitEditDto>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
