using iPassport.Application.Resources;
using iPassport.Domain.Dtos.DtoValidator;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace iPassport.Test.Services.DtoValidator
{
    [TestClass]
    public class UserImportDtoValidatorTest
    {
        UserImportDtoValidator _validator;
        public Resource resource { get; private set; }

        public UserImportDtoValidatorTest()
        { }

        [TestInitialize]
        public void Setup()
        {
            resource = ResourceFactory.Create();
            _validator = new(ResourceFactory.GetStringLocalizer());
        }

        //[TestMethod]
        //public void FullNameNullRequired()
        //{
        //    // Act
        //    var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithFullNameNull());

        //    // Assert
        //    Assert.AreEqual(validationResult.Errors.Count(), 1);
        //    Assert.AreEqual(validationResult.Errors.Single().ErrorMessage, resource.GetMessage("FieldRequired"));
        //    Assert.AreEqual(validationResult.Errors.Single().PropertyName, "FullName");
        //}

        //[TestMethod]
        //public void FullNameEmptyRequired()
        //{
        //    // Act
        //    var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithFullNameEmpty());

        //    // Assert
        //    Assert.AreEqual(validationResult.Errors.Count(), 1);
        //    Assert.AreEqual(validationResult.Errors.Single().ErrorMessage, resource.GetMessage("FieldRequired"));
        //    Assert.AreEqual(validationResult.Errors.Single().PropertyName, "FullName");
        //}

        //[TestMethod]
        //public void BirthdayCannotBeHiggerThenCurrentDate()
        //{
        //    // Act
        //    var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithBrithdayHiggerThenActualDate());

        //    // Assert
        //    Assert.AreEqual(validationResult.Errors.Count(), 1);
        //    Assert.AreEqual(validationResult.Errors.Single().ErrorMessage, resource.GetMessage("BirthdayCannotBeHiggerThenActualDate"));
        //    Assert.AreEqual(validationResult.Errors.Single().PropertyName, "Birthday");
        //}

        //[TestMethod]
        //public void CpfNullRequired()
        //{
        //    // Act
        //    var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCpfNull());

        //    // Assert
        //    Assert.AreEqual(validationResult.Errors.Count(), 1);
        //    Assert.AreEqual(validationResult.Errors.Single().ErrorMessage, resource.GetMessage("FieldRequired"));
        //    Assert.AreEqual(validationResult.Errors.Single().PropertyName, "Cpf");
        //}

        //[TestMethod]
        //public void CpfEmptyRequired()
        //{
        //    // Act
        //    var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCpfEmpty());

        //    // Assert
        //    Assert.AreEqual(validationResult.Errors.Count(), 1);
        //    Assert.AreEqual(validationResult.Errors.Single().ErrorMessage, resource.GetMessage("FieldRequired"));
        //    Assert.AreEqual(validationResult.Errors.Single().PropertyName, "Cpf");
        //}

        //[TestMethod]
        //public void CpfLength()
        //{
        //    // Act
        //    var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCpfLowLenght());

        //    // Assert
        //    Assert.AreEqual(validationResult.Errors.Count(), 2);
        //    Assert.AreEqual(validationResult.Errors.First().ErrorMessage, string.Format(resource.GetMessage("FieldMustHaveANumberOfDigits"), 8));
        //    Assert.AreEqual(validationResult.Errors.First().PropertyName, "Cpf");

        //    Assert.AreEqual(validationResult.Errors.Last().ErrorMessage, string.Format(resource.GetMessage("FieldValueInformedIsNotValidMasc"), resource.GetMessage("ColumnNameImportFileCpf")));
        //    Assert.AreEqual(validationResult.Errors.Last().PropertyName, "Cpf");
        //}
    }
}
