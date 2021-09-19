using iPassport.Api.Models.Requests.Company;
using iPassport.Api.Models.Validators.Company;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class AssociateSubsidiariesRequestValidatorTest
    {
        AssociateSubsidiariesRequestValidator _validator;
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
            Assert.IsTrue(_validator.Validate(new AssociateSubsidiariesRequest() { AssociateAll = true, Subsidiaries = null }).IsValid);
        }

        [TestMethod]
        public void InvalidAssociateAll()
        {
            // Act
            var validationResult = _validator.Validate(new AssociateSubsidiariesRequest() { AssociateAll = true, Subsidiaries = new List<Guid>() { Guid.NewGuid() } });

            var expetedMessage = resource.GetMessage("AssociateAllMustBeNullWhenSubsidiariesIsNotNull");

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void SubsidiariesRequired()
        {
            // Act
            var validationResult = _validator.Validate(new AssociateSubsidiariesRequest() { AssociateAll = false, Subsidiaries = null });

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("Subsidiaries"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void AllNull()
        {
            // Act
            var validationResult = _validator.Validate(new AssociateSubsidiariesRequest() { AssociateAll = null, Subsidiaries = null });

            var expetedMessage1 = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("Subsidiaries"));
            var expetedMessage2 = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("AssociateAll"));

            // Assert
            Assert.AreEqual(2, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage1, validationResult.Errors.FirstOrDefault(x => x.ErrorMessage.Contains(expetedMessage1)).ErrorMessage);
            Assert.AreEqual(expetedMessage2, validationResult.Errors.FirstOrDefault(x => x.ErrorMessage.Contains(expetedMessage2)).ErrorMessage);
        }
    }
}
