using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Validators.Plans;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class StateCreateRequestValidatorTest
    {
        StateCreateRequestValidator _validator;
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
            Assert.IsTrue(_validator.Validate(new StateCreateRequest() { Name = "test", IbgeCode = 1, Population = 10, CountryId = Guid.NewGuid() }).IsValid);
        }

        [TestMethod]
        public void InvalidPopulation()
        {
            // Act
            var validationResult = _validator.Validate(new StateCreateRequest() { Name = "test", IbgeCode = 1, Population = 0, CountryId = Guid.NewGuid() });

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Population"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void InvalidIbgeCode()
        {
            // Act
            var validationResult = _validator.Validate(new StateCreateRequest() { Name = "test", IbgeCode = 0, Population = 1, CountryId = Guid.NewGuid() });

            var expetedMessage1 = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("IbgeCode"));
            var expetedMessage2 = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("IbgeCode"));

            // Assert
            Assert.AreEqual(2, validationResult.Errors.Count());

            Assert.IsNotNull(validationResult.Errors.FirstOrDefault(x => x.ErrorMessage.Contains(expetedMessage1)));
            Assert.IsNotNull(validationResult.Errors.FirstOrDefault(x => x.ErrorMessage.Contains(expetedMessage2)));
        }
    }
}
