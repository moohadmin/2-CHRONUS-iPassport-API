using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class IndicatorServiceTest
    {
        Mock<IUserVaccineRepository> _userVaccineRepository;
        IIndicatorService _service;

        [TestInitialize]
        public void Setup()
        {
            _userVaccineRepository = new Mock<IUserVaccineRepository>();
            _service = new IndicatorService(_userVaccineRepository.Object);
        }

        [TestMethod]
        public void GetByNameInitals()
        {
            var mockFilter = Mock.Of<GetVaccinatedCountFilter>();

            // Arrange
            _userVaccineRepository.Setup(r => r.GetVaccinatedCount(It.IsAny<GetVaccinatedCountFilter>()).Result).Returns(new List<VaccineIndicatorDto>());

            // Act
            var result = _service.GetVaccinatedCount(mockFilter);

            // Assert
            _userVaccineRepository.Verify(a => a.GetVaccinatedCount(It.IsAny<GetVaccinatedCountFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}
