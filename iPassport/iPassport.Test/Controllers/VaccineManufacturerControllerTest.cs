using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Domain.Filters;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Controllers
{
    [TestClass]
    public class VaccineManufacturerControllerTest
    {
        Mock<IVaccineManufacturerService> _mockService;
        IMapper _mapper;
        VaccineManufacturerController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IVaccineManufacturerService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new VaccineManufacturerController(_mapper, _mockService.Object) { };
        }

        [TestMethod]
        public void GetByNameInitals_MustReturnOk()
        {
            var seed = VaccineManufacturerSeed.GetPagedVaccineManufacturer();
            var mockRequest = Mock.Of<GetByNameInitalsPagedRequest>();

            // Arrange
            _mockService.Setup(r => r.GetByNameInitals(It.IsAny<GetByNameInitalsPagedFilter>()).Result).Returns(new PagedResponseApi(true, "Test Success!", 1, 3, 10, 300, seed));

            // Act
            var result = _controller.GetByNameInitals(mockRequest);

            // Assert
            _mockService.Verify(a => a.GetByNameInitals(It.IsAny<GetByNameInitalsPagedFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
