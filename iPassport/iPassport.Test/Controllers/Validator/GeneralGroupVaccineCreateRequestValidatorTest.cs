using iPassport.Api.Models.Requests.Vaccine;
using iPassport.Api.Models.Validators.Vaccines;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class GeneralGroupVaccineCreateRequestValidatorTest
    {
        GeneralGroupVaccineCreateRequestValidator _validator;
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
                new GeneralGroupVaccineCreateRequest()
                {
                    PeriodType = EVaccinePeriodType.Variable,
                    RequiredDoses = 1,
                    TimeNextDoseMax = 2,
                    TimeNextDoseMin = 3
                }).IsValid);
        }

        [TestMethod]
        public void PeriodTypeRequired()
        {
            var seed = new GeneralGroupVaccineCreateRequest()
            {
                PeriodType = null,
                RequiredDoses = 2,
                TimeNextDoseMax = 2,
                TimeNextDoseMin = 3
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("VaccinePeriodType"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void RequiredDosesRequired()
        {
            var seed = new GeneralGroupVaccineCreateRequest()
            {
                PeriodType = EVaccinePeriodType.Variable,
                RequiredDoses = null,
                TimeNextDoseMax = 2,
                TimeNextDoseMin = 3
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("VaccineRequiredDoses"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void TimeNextDoseMaxDosesRequired()
        {
            var seed = new GeneralGroupVaccineCreateRequest()
            {
                PeriodType = EVaccinePeriodType.Variable,
                RequiredDoses = 2,
                TimeNextDoseMax = null,
                TimeNextDoseMin = 3
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("VaccineMaxTimeNextDose"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void TimeNextDoseMinRequired()
        {
            var seed = new GeneralGroupVaccineCreateRequest()
            {
                PeriodType = EVaccinePeriodType.Variable,
                RequiredDoses = 2,
                TimeNextDoseMax = 1,
                TimeNextDoseMin = null
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("VaccineMinTimeNextDose"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }
    }
}
