using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Validators.Plans;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class CountryCreateRequestValidatorTest
    {
        CountryCreateRequestValidator _validator;
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
            Assert.IsTrue(_validator.Validate(new CountryCreateRequest() { Acronym = "test", ExternalCode = "123123", Population = 1111, Name = "test" }).IsValid);
        }

        [TestMethod]
        public void InvalidPopulation()
        {
            // Act
            var validationResult = _validator.Validate(new CountryCreateRequest() { Name = "test", Acronym = "Test", ExternalCode="12312312", Population = 0 });

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Population"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }
    }
}
