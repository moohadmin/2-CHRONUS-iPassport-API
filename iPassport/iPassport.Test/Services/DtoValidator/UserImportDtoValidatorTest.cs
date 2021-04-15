using iPassport.Application.Resources;
using iPassport.Domain.Dtos.DtoValidator;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace iPassport.Test.Services.DtoValidator
{
    //[TestClass]
    //public class UserImportDtoValidatorTest
    //{
    //    UserImportDtoValidator _validator;
    //    public Resource resource { get; private set; }

    //    public UserImportDtoValidatorTest()
    //    { }

    //    [TestInitialize]
    //    public void Setup()
    //    {
    //        resource = ResourceFactory.Create();
    //        _validator = new(ResourceFactory.GetStringLocalizer());
    //    }

    //    [TestMethod]
    //    public void FullNameNullRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithFullNameNull());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.Single().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.Single().PropertyName, "FullName");
    //    }

    //    [TestMethod]
    //    public void FullNameEmptyRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithFullNameEmpty());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.Single().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.Single().PropertyName, "FullName");
    //    }

    //    [TestMethod]
    //    public void BirthdayCannotBeHiggerThenCurrentDate()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithBrithdayHiggerThenActualDate());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.Single().ErrorMessage, resource.GetMessage("BirthdayCannotBeHiggerThenActualDate"));
    //        Assert.AreEqual(validationResult.Errors.Single().PropertyName, "Birthday");
    //    }

    //    [TestMethod]
    //    public void CpfAndCnsNullsRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCpfAndNulls());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 2);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Cpf");
    //        Assert.AreEqual(validationResult.Errors[1].ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors[1].PropertyName, "Cns");
    //    }

    //    [TestMethod]
    //    public void CpfAndCnsEmptyRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCpfAndCnsEmpty());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 2);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Cpf");
    //        Assert.AreEqual(validationResult.Errors[1].ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors[1].PropertyName, "Cns");
    //    }

    //    [TestMethod]
    //    public void MustHaveNoErrosWhenCpfIsNullAndCnsIsNot()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCpfNull());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 0);
    //    }

    //    [TestMethod]
    //    public void MustHaveNoErrosWhenCnsIsEmptyAndCpfIsNot()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCnsEmpty());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 0);
    //    }

    //    [TestMethod]
    //    public void MustHaveNoErrosWhenCnsIsNullAndCpfIsNot()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCnsNull());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 0);
    //    }

    //    [TestMethod]
    //    public void MustHaveNoErrosWhenCpfIsEmptyAndCnsIsNot()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCpfEmpty());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 0);
    //    }

    //    [TestMethod]
    //    public void CpfLength()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCpfLowLenght());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, string.Format(resource.GetMessage("FieldMustHaveANumberOfDigits"), 8));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Cpf");
    //    }

    //    [TestMethod]
    //    public void CnsLength()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCnsLowLenght());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, string.Format(resource.GetMessage("FieldMustHaveANumberOfDigits"), 15));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Cns");
    //    }

    //    [TestMethod]
    //    public void PhoneNumberLength()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithPhoneNumberLowLenght());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("NonstandardField"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "PhoneNumber");
    //    }

    //    [TestMethod]
    //    public void EmailInvalid()
    //    {
    //        // Act
    //       var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithInvalidEmail());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("NonstandardField"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Email");
    //    }

    //    [TestMethod]
    //    public void CepLenght()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCepLowLenght());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, string.Format(resource.GetMessage("FieldMustHaveANumberOfDigits"), 8));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Cep");
    //    }

    //    [TestMethod]
    //    public void CityNullRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCityNull());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "City");
    //    }

    //    [TestMethod]
    //    public void CityEmptyRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCityEmpty());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "City");
    //    }

    //    [TestMethod]
    //    public void StateNullRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithStateNull());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "State");
    //    }

    //    [TestMethod]
    //    public void StateEmptyRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithStateEmpty());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "State");
    //    }

    //    [TestMethod]
    //    public void CountryNullRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCountryNull());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Country");
    //    }

    //    [TestMethod]
    //    public void CountryEmptyRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithCountryEmpty());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Country");
    //    }

    //    [TestMethod]
    //    public void WasCovidInfectedNullRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithWasCovidInfectedNull());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "WasCovidInfected");
    //    }

    //    [TestMethod]
    //    public void WasCovidInfectedEmptyRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithWasCovidInfectedEmpty());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "WasCovidInfected");
    //    }

    //    [TestMethod]
    //    public void WasCovidInfectedNonStandard()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithWasCovidInfectedInvalidValue());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("NonstandardField"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "WasCovidInfected");
    //    }

    //    [TestMethod]
    //    public void WasTestPerformedNullRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithWasTestPerformedNull());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "WasTestPerformed");
    //    }

    //    [TestMethod]
    //    public void WasTestPerformedEmptyRequired()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithWasTestPerformedEmpty());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "WasTestPerformed");
    //    }

    //    [TestMethod]
    //    public void WasTestPerformedNonStandard()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithWasTestPerformedInvalidValue());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("NonstandardField"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "WasTestPerformed");
    //    }

    //    [TestMethod]
    //    public void TestDateNotNullWhenWasTestPerformed()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithTestDateNullAndWasTestPerformed());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "TestDate");
    //    }

    //    [TestMethod]
    //    public void TestDateCannotBeHigherThenCurrentDateAndResultDateCannotBeLessThenTestDate()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithTestDateHigherThenCurrentDate());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 2);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("TestDateCannotBeHiggerThenActualDate"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "TestDate");
    //        Assert.AreEqual(validationResult.Errors[1].ErrorMessage, resource.GetMessage("TestResultDateCannotBeEqualOrLessThenTestDate"));
    //        Assert.AreEqual(validationResult.Errors[1].PropertyName, "ResultDate");
    //    }

    //    [TestMethod]
    //    public void ResultNullRequiredWhenTestPerformed()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithResultNull());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Result");
    //    }

    //    [TestMethod]
    //    public void ResultEmptyRequiredWhenTestPerformed()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithResultEmpty());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Result");
    //    }

    //    [TestMethod]
    //    public void ResultNonstandardValue()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithResultNonstandardValue());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("NonstandardField"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "Result");
    //    }

    //    [TestMethod]
    //    public void ResultDateRequiredWhenResultHasValue()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithResultDateNullAndResultHasValue());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("FieldRequired"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "ResultDate");
    //    }

    //    [TestMethod]
    //    public void ResultDateCannotBeHigherThenCurrentDate()
    //    {
    //        // Act
    //        var validationResult = _validator.Validate(UserImportDtoSeed.UserImportDtoWithResultDateHigherThenCurrentDate());

    //        // Assert
    //        Assert.AreEqual(validationResult.Errors.Count(), 1);
    //        Assert.AreEqual(validationResult.Errors.First().ErrorMessage, resource.GetMessage("TestResultDateCannotBeHiggerThenCurrentDate"));
    //        Assert.AreEqual(validationResult.Errors.First().PropertyName, "ResultDate");
    //    }
    //}
}
