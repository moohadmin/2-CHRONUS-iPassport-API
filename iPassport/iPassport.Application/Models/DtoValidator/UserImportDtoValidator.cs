using FluentValidation;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System;
using System.Text.RegularExpressions;

namespace iPassport.Domain.Dtos.DtoValidator
{
    public class UserImportDtoValidator : AbstractValidator<UserImportDto>
    {
        public UserImportDtoValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.FullName)
                .NotNull().WithMessage(localizer["FieldRequired"])
                .NotEmpty().WithMessage(localizer["FieldRequired"]);
            
            RuleFor(x => x.Birthday)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(localizer["BirthdayCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.Cpf)
                .NotNull().WithMessage(localizer["FieldRequired"])
                .NotEmpty().WithMessage(localizer["FieldRequired"])
                .Length(11).When(x => !string.IsNullOrWhiteSpace(x.Cpf)).WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 8))
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.Cpf)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.Cpf)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.Cns)
                .NotNull().WithMessage(localizer["FieldRequired"])
                .NotEmpty().WithMessage(localizer["FieldRequired"])
                .Length(15).When(x => !string.IsNullOrWhiteSpace(x.Cns)).WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 15));

            RuleFor(x => x.Cep)
                .Length(8).When(x => !string.IsNullOrWhiteSpace(x.Cns)).WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 8));

            RuleFor(x => x.CountryCode)
                .NotNull().WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.PhoneNumber)
                .NotNull().WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.Email)
                .EmailAddress().When(x => x != null).WithMessage(string.Format(localizer["FieldInvalid"], localizer["ColumnNameImportFileEmail"]));

            RuleFor(x => x.City)
                .NotNull().WithMessage(localizer["FieldRequired"])
                .NotEmpty().WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.State)
                .NotNull().WithMessage(localizer["FieldRequired"])
                .NotEmpty().WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.Country)
                .NotNull().WithMessage(localizer["FieldRequired"])
                .NotEmpty().WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.WasCovidInfected)
                .NotNull().WithMessage(localizer["FieldRequired"])
                .NotEmpty().WithMessage(localizer["FieldRequired"])
                .Must(x => x.ToUpper() == Constants.CONST_SIM_VALUE || x.ToUpper() == Constants.CONST_NAO_VALUE || x.ToUpper() == Constants.CONST_NENHUM_VALUE).WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.WasTestPerformed)
                .NotNull().WithMessage(localizer["FieldRequired"])
                .NotEmpty().WithMessage(localizer["FieldRequired"])
                .Must(x => x.ToUpper() == Constants.CONST_SIM_VALUE || x.ToUpper() == Constants.CONST_NAO_VALUE || x.ToUpper() == Constants.CONST_NENHUM_VALUE).WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.TestDate)
                .NotEmpty().When(x => x.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE).WithMessage(localizer["FieldRequired"])
                .LessThanOrEqualTo(DateTime.UtcNow).When(x => x.TestDate.HasValue).WithMessage(localizer["TestDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.Result)
                .NotNull().When(x => x.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE).WithMessage(localizer["FieldRequired"])
                .Must(x => x.ToUpper() == Constants.CONST_POSITIVO_VALUE || x.ToUpper() == Constants.CONST_NEGATIVO_VALUE || x.ToUpper() == Constants.CONST_NENHUM_VALUE).WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.ResultDate)
                .NotNull().When(x => x.ResultBool.HasValue).WithMessage(localizer["FieldRequired"])
                .LessThanOrEqualTo(x => x.TestDate).When(x => x.ResultBool.HasValue).WithMessage(localizer["TestDateCannotBeHiggerThenActualDate"]);

            // VACCINE UNIQUE DOSE

            RuleFor(x => x.VaccineNameUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData).WithMessage(localizer["FieldRequired"])
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationUniqueDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationUniqueDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationUniqueDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneUniqueDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneUniqueDose)).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityIneUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose)).WithMessage(localizer["FieldRequired"]);
            
            RuleFor(x => x.HealthUnityCodeUniqueDose)
                .NotNull().When(x => string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneUniqueDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneUniqueDose)).WithMessage(localizer["FieldRequired"]);

            // VACCINE FIRST DOSE

            RuleFor(x => x.VaccineNameFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineFirstDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineFirstDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineFirstDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineFirstDoseData).WithMessage(localizer["FieldRequired"])
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationFirstDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationFirstDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationFirstDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneFirstDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneFirstDose)).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityIneFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose)).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCodeFirstDose)
                .NotNull().When(x => string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneFirstDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneFirstDose)).WithMessage(localizer["FieldRequired"]);

            // VACCINE SECOND DOSE

            RuleFor(x => x.VaccineNameSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineSecondDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineSecondDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineSecondDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineSecondDoseData).WithMessage(localizer["FieldRequired"])
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationSecondDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationSecondDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationSecondDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneSecondDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneSecondDose)).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityIneSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose)).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCodeSecondDose)
                .NotNull().When(x => string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneSecondDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneSecondDose)).WithMessage(localizer["FieldRequired"]);

            // VACCINE THIRD DOSE

            RuleFor(x => x.VaccineNameThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineThirdDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineThirdDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineThirdDoseData).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineThirdDoseData).WithMessage(localizer["FieldRequired"])
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationThirdDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationThirdDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationThirdDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneThirdDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineThirdDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneThirdDose)).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityIneThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjThirdDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => x.HasVaccineThirdDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjThirdDose)).WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCodeSecondDose)
                .NotNull().When(x => string.IsNullOrWhiteSpace(x.HealthUnityCnpjThirdDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneThirdDose)).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => string.IsNullOrWhiteSpace(x.HealthUnityCnpjThirdDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneThirdDose)).WithMessage(localizer["FieldRequired"]);
        }
    }
}
