using AutoMapper;
using iPassport.Application.Models;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Repositories;
using iPassport.Test.Settings.Factories;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class HealthUnitTypeServiceTest
    {
        Mock<IHealthUnitTypeRepository> _mockRepository;
        HealthUnitTypeService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IHealthUnitTypeRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _service = new HealthUnitTypeService(_mapper, _mockRepository.Object, _mockLocalizer.Object);
        }

        [TestMethod]
        public void GetAll_MustReturnOk()
        {
            // Arrange
            _mockRepository.Setup(r => r.FindAll());

            // Act
            var result = _service.GetAll();

            // Assert
            _mockRepository.Verify(a => a.FindAll(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
        }
    }
}
