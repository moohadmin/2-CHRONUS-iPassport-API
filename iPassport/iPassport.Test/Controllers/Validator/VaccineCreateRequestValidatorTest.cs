using iPassport.Api.Models.Requests.Vaccine;
using iPassport.Api.Models.Validators.Vaccines;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class VaccineCreateRequestValidatorTest
    {
        VaccineCreateRequestValidator _validator;
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
                new VaccineCreateRequest()
                {
                    DosageType = EVaccineDosageType.GeneralGroup,
                    Diseases = new List<Guid>() { Guid.NewGuid()},
                    ExpirationTimeInMonths = 120,
                    ImmunizationTimeInDays = 30,
                    GeneralGroupVaccine = new GeneralGroupVaccineCreateRequest()
                    {
                        PeriodType = EVaccinePeriodType.Fixed,
                        RequiredDoses = 2,
                        TimeNextDoseMin = 1
                    },
                    IsActive = true,
                    Manufacturer = Guid.NewGuid(),
                    Name = "Test"
                }).IsValid);
        }

        [TestMethod]
        public void NameRequired()
        {
            var seed = new VaccineCreateRequest()
            {
                DosageType = EVaccineDosageType.GeneralGroup,
                Diseases = new List<Guid>() { Guid.NewGuid() },
                ExpirationTimeInMonths = 120,
                ImmunizationTimeInDays = 30,
                GeneralGroupVaccine = new GeneralGroupVaccineCreateRequest()
                {
                    PeriodType = EVaccinePeriodType.Fixed,
                    RequiredDoses = 2,
                    TimeNextDoseMin = 1
                },
                IsActive = true,
                Manufacturer = Guid.NewGuid(),
                Name = null
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("VaccineName"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void ManufacturerRequired()
        {
            var seed = new VaccineCreateRequest()
            {
                DosageType = EVaccineDosageType.GeneralGroup,
                Diseases = new List<Guid>() { Guid.NewGuid() },
                ExpirationTimeInMonths = 120,
                ImmunizationTimeInDays = 30,
                GeneralGroupVaccine = new GeneralGroupVaccineCreateRequest()
                {
                    PeriodType = EVaccinePeriodType.Fixed,
                    RequiredDoses = 2,
                    TimeNextDoseMin = 1
                },
                IsActive = true,
                Manufacturer = null,
                Name = "test"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("VaccineManufacturer"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void DiseasesRequired()
        {
            var seed = new VaccineCreateRequest()
            {
                DosageType = EVaccineDosageType.GeneralGroup,
                Diseases = null,
                ExpirationTimeInMonths = 120,
                ImmunizationTimeInDays = 30,
                GeneralGroupVaccine = new GeneralGroupVaccineCreateRequest()
                {
                    PeriodType = EVaccinePeriodType.Fixed,
                    RequiredDoses = 2,
                    TimeNextDoseMin = 1
                },
                IsActive = true,
                Manufacturer = Guid.NewGuid(),
                Name = "test"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("Diseases"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void ExpirationTimeInMonthsRequired()
        {
            var seed = new VaccineCreateRequest()
            {
                DosageType = EVaccineDosageType.GeneralGroup,
                Diseases = new List<Guid>() { Guid.NewGuid() },
                ExpirationTimeInMonths = null,
                ImmunizationTimeInDays = 30,
                GeneralGroupVaccine = new GeneralGroupVaccineCreateRequest()
                {
                    PeriodType = EVaccinePeriodType.Fixed,
                    RequiredDoses = 2,
                    TimeNextDoseMin = 1
                },
                IsActive = true,
                Manufacturer = Guid.NewGuid(),
                Name = "test"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("VaccineExpirationTimeInMonths"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void ImmunizationTimeInDaysRequired()
        {
            var seed = new VaccineCreateRequest()
            {
                DosageType = EVaccineDosageType.GeneralGroup,
                Diseases = new List<Guid>() { Guid.NewGuid() },
                ExpirationTimeInMonths = 120,
                ImmunizationTimeInDays = null,
                GeneralGroupVaccine = new GeneralGroupVaccineCreateRequest()
                {
                    PeriodType = EVaccinePeriodType.Fixed,
                    RequiredDoses = 2,
                    TimeNextDoseMin = 1
                },
                IsActive = true,
                Manufacturer = Guid.NewGuid(),
                Name = "test"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("VaccineImmunizationTimeInDays"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void DosageTypeRequired()
        {
            var seed = new VaccineCreateRequest()
            {
                DosageType = null,
                Diseases = new List<Guid>() { Guid.NewGuid() },
                ExpirationTimeInMonths = 120,
                ImmunizationTimeInDays = 30,
                GeneralGroupVaccine = new GeneralGroupVaccineCreateRequest()
                {
                    PeriodType = EVaccinePeriodType.Fixed,
                    RequiredDoses = 2,
                    TimeNextDoseMin = 1
                },
                IsActive = true,
                Manufacturer = Guid.NewGuid(),
                Name = "test"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("VaccineDosageType"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }
    }
}
