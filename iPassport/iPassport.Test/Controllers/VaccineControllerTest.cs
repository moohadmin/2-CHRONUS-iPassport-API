using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Vaccine;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
            _mockService.Setup(r => r.GetByManufacturerId(It.IsAny<GetVaccineByManufacturerFilter>()).Result).Returns(new ResponseApi(true, "Test Success!", seed));

            // Act
            var result = _controller.GetByManufacturerId(mockRequest);

            // Assert
            _mockService.Verify(a => a.GetByManufacturerId(It.IsAny<GetVaccineByManufacturerFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetPagged_MustReturnOk()
        {
            var seed = VaccineSeed.GetVaccines();
            var mockRequest = Mock.Of<GetPagedVaccinesRequest>();

            // Arrange
            _mockService.Setup(r => r.GetPagged(It.IsAny<GetPagedVaccinesFilter>()).Result).Returns(new ResponseApi(true, "Test Success!", seed));

            // Act
            var result = _controller.GetPagged(mockRequest);

            // Assert
            _mockService.Verify(a => a.GetPagged(It.IsAny<GetPagedVaccinesFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Add_MustReturnOk()
        {
            var mockRequest = Mock.Of<VaccineCreateRequest>(x => x.DosageType == EVaccineDosageType.GeneralGroup && x.GeneralGroupVaccine == new GeneralGroupVaccineCreateRequest() { PeriodType = EVaccinePeriodType.Variable, RequiredDoses = 2, TimeNextDoseMax = 2, TimeNextDoseMin = 1 });

            // Arrange
            _mockService.Setup(r => r.Add(It.IsAny<VaccineDto>()).Result).Returns(new ResponseApi(true, "Test Success!", null));

            // Act
            var result = _controller.Add(mockRequest);

            // Assert
            _mockService.Verify(a => a.Add(It.IsAny<VaccineDto>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
