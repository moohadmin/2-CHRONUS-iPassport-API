using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Validators.Plans;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class PlanCreateRequestValidatorTest
    {
        PlanCreateRequestValidator _validator;
        public Resource resource { get; private set; }

        [TestInitialize]
        public void Setup()
        {
            resource = ResourceFactory.Create();
            _validator = new(ResourceFactory.GetStringLocalizer());
        }

        [TestMethod]
        public void Success()
        {
            Assert.IsTrue(_validator.Validate(
                new PlanCreateRequest() {
                    Active = true,
                    ColorEnd = "3cc143",
                    ColorStart = "98a190",
                    Description = "test plan",
                    Observation = "test",
                    Price = 3.5m,
                    Type = "test"
                }).IsValid);
        }

        [TestMethod]
        [DataRow("123")]
        [DataRow("aaaaaaaaaa")]
        [DataRow("!@#$%¨&")]
        public void InvalidColorStart(string color)
        {
            var seed = new PlanCreateRequest()
            {
                Active = true,
                ColorEnd = "3cc143",
                ColorStart = color,
                Description = "test plan",
                Observation = "test",
                Price = 3.5m,
                Type = "test"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), "ColorStart");

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("123")]
        [DataRow("aaaaaaaaaa")]
        [DataRow("!@#$%¨&")]
        public void InvalidColorEnd(string color)
        {
            var seed = new PlanCreateRequest()
            {
                Active = true,
                ColorEnd = color,
                ColorStart = "3cc143",
                Description = "test plan",
                Observation = "test",
                Price = 3.5m,
                Type = "test"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), "ColorEnd");

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }
    }
}
