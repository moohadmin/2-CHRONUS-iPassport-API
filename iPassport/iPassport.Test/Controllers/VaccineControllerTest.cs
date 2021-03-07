using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
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
    public class VaccineControllerTest
    {
        Mock<IVaccineService> _mockService;
        IMapper _mapper;
        VaccineController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IVaccineService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new VaccineController(_mapper, _mockService.Object) { };
        }

        [TestMethod]
        public void GetVaccinatedCount_MustReturnOk()
        {
            var seed = VaccineSeed.GetVaccineIndicatorDtos();
            var mockRequest = Mock.Of<GetVaccinatedCountRequest>();

            // Arrange
            _mockService.Setup(r => r.GetVaccinatedCount(It.IsAny<GetVaccinatedCountFilter>()).Result).Returns(new ResponseApi(true, "Test Success!", seed));

            // Act
            var result = _controller.GetVaccinatedCount(mockRequest);

            // Assert
            _mockService.Verify(a => a.GetVaccinatedCount(It.IsAny<GetVaccinatedCountFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetByManufacturerId_MustReturnOk()
        {
            var seed = VaccineSeed.GetVaccines();
            var mockRequest = Mock.Of<GetPagedVaccinesByManufacuterRequest>();

            // Arrange
            _mockService.Setup(r => r.GetByManufacturerId(It.IsAny<GetByIdAndNamePartsPagedFilter>()).Result).Returns(new ResponseApi(true, "Test Success!", seed));

            // Act
            var result = _controller.GetByManufacturerId(mockRequest);

            // Assert
            _mockService.Verify(a => a.GetByManufacturerId(It.IsAny<GetByIdAndNamePartsPagedFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
