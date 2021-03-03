using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class VaccineServiceTest
    {
        Mock<IUserVaccineRepository> _userVaccineRepository;
        IVaccineService _service;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _userVaccineRepository = new Mock<IUserVaccineRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();

            _service = new VaccineService(_userVaccineRepository.Object, _mockLocalizer.Object);
        }

        [TestMethod]
        public void GetByNameInitals()
        {
            var mockFilter = Mock.Of<GetVaccinatedCountFilter>();

            // Arrange
            _userVaccineRepository.Setup(r => r.GetVaccinatedCount(It.IsAny<GetVaccinatedCountFilter>()).Result).Returns(3);

            // Act
            var result = _service.GetVaccinatedCount(mockFilter);

            // Assert
            _userVaccineRepository.Verify(a => a.GetVaccinatedCount(It.IsAny<GetVaccinatedCountFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}
