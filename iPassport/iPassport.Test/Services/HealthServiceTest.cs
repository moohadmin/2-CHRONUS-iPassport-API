using AutoMapper;
using iPassport.Application.Models;
using iPassport.Application.Services;
using iPassport.Domain.Repositories;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class HealthServiceTest
    {
        Mock<IHealthRepository> _mockRepository;
        HealthService _service;
        public IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IHealthRepository>();
            _service = new HealthService(_mapper, _mockRepository.Object);
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
