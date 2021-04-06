using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Test.Controllers
{
    [TestClass]
    public class ProfileControllerTest
    {
        Mock<IProfileService> _mockService;
        IMapper _mapper;
        ProfileController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IProfileService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new ProfileController(_mockService.Object);
        }

        [TestMethod]
        public void GetByStateAndNameParts_MustReturnOk()
        {
            // Arrange
            _mockService.Setup(r => r.GetAll().Result).Returns(new ResponseApi(true, "test", _mapper.Map<IList<ProfileViewModel>>(ProfileSeed.GetProfiles())));

            // Act
            var result = _controller.GetAll();

            // Assert
            _mockService.Verify(a => a.GetAll());
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
