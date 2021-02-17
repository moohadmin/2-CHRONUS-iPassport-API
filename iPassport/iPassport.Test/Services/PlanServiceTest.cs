using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class PlanServiceTest
    {
        Mock<IPlanRepository> _mockRepository;
        IPlanService _service;
        IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IPlanRepository>();
            _service = new PlanService(_mapper, _mockRepository.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            var seed = PlanSeed.GetPlans();

            // Arrange
            _mockRepository.Setup(r => r.FindAll().Result).Returns(seed);

            // Act
            var result = _service.GetAll();

            // Assert
            _mockRepository.Verify(a => a.FindAll(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(IList<PlanViewModel>));
        }

        [TestMethod]
        public void Add()
        {
            var mockRequest = Mock.Of<PlanCreateDto>();

            // Arrange
            _mockRepository.Setup(r => r.InsertAsync(It.IsAny<Plan>()));

            // Act
            var result = _service.Add(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.InsertAsync(It.IsAny<Plan>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}
