using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Application.Interfaces;
using iPassport.Domain.Filters;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Controllers
{
    [TestClass]
    public class GenderControllerTest
    {
        Mock<IGenderService> _mockService;
        IMapper _mapper;
        GenderController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IGenderService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new GenderController(_mapper, _mockService.Object);
        }

        [TestMethod]
        public void GetByNameParts_MustReturnOk()
        {
            var mockRequest = Mock.Of<GetByNamePartsPagedRequest>();

            // Arrange
            _mockService.Setup(r => r.FindByNameParts(It.IsAny<GetByNamePartsPagedFilter>()));

            // Act
            var result = _controller.GetByNameParts(mockRequest);

            // Assert
            _mockService.Verify(a => a.FindByNameParts(It.IsAny<GetByNamePartsPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
