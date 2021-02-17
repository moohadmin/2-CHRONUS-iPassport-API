using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Controllers
{
    [TestClass]
    public class PlanControllerTest
    {
        Mock<IPlanService> _mockService;
        IMapper _mapper;
        PlanController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IPlanService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new PlanController(_mapper, _mockService.Object) { };
        }

        [TestMethod]
        public void Post_MustReturnOk()
        {
            var mockRequest = Mock.Of<PlanCreateRequest>();

            // Arrange
            _mockService.Setup(r => r.Add(It.IsAny<PlanCreateDto>()));

            // Act
            var result = _controller.Post(mockRequest);

            // Assert
            _mockService.Verify(a => a.Add(It.IsAny<PlanCreateDto>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetAll_MustReturnOk()
        {
            var seed = PlanSeed.GetPlans();

            // Arrange
            _mockService.Setup(r => r.GetAll()).Returns(Task.FromResult(new ResponseApi(true, "Test Success!", seed)));

            // Act
            var result = _controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
