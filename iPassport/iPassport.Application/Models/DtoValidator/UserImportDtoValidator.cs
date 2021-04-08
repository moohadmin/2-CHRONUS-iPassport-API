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
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(localizer["FieldRequired"])
                .NotEmpty()
                    .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.Birthday)
                .LessThanOrEqualTo(DateTime.UtcNow)
                    .WithMessage(localizer["BirthdayCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.Cpf).Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(localizer["FieldRequired"])
                .NotEmpty()
                    .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.Cpf)
                .Length(11)
                    .When(x => !string.IsNullOrWhiteSpace(x.Cpf))
                    .WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 8));

            RuleFor(x => x.Cpf)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$"))
                .When(x => !string.IsNullOrWhiteSpace(x.Cpf))
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.Cpf)
                .Must(x => CpfUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.Cpf))
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

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

            RuleFor(x => x.WasCovidInfected).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(localizer["FieldRequired"])
                .NotEmpty().WithMessage(localizer["FieldRequired"])
                .Must(x => x.ToUpper() == Constants.CONST_SIM_VALUE || x.ToUpper() == Constants.CONST_NAO_VALUE || x.ToUpper() == Constants.CONST_NENHUM_VALUE).WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.WasTestPerformed)
                .NotNull().WithMessage(localizer["FieldRequired"])
                .NotEmpty().WithMessage(localizer["FieldRequired"])
                .Must(x => x.ToUpper() == Constants.CONST_SIM_VALUE || x.ToUpper() == Constants.CONST_NAO_VALUE || x.ToUpper() == Constants.CONST_NENHUM_VALUE)
                    .When(x => !string.IsNullOrWhiteSpace(x.WasTestPerformed))
                    .WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.TestDate)
                .NotEmpty().When(x => x.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE).WithMessage(localizer["FieldRequired"])
                .LessThanOrEqualTo(DateTime.UtcNow).When(x => x.TestDate.HasValue).WithMessage(localizer["TestDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.Result).Cascade(CascadeMode.Stop)
                .NotNull().When(x => !string.IsNullOrWhiteSpace(x.WasTestPerformed) && x.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE).WithMessage(localizer["FieldRequired"])
                .NotEmpty().When(x => !string.IsNullOrWhiteSpace(x.WasTestPerformed) && x.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE).WithMessage(localizer["FieldRequired"])
                .Must(x => x.ToUpper() == Constants.CONST_POSITIVO_VALUE || x.ToUpper() == Constants.CONST_NEGATIVO_VALUE || x.ToUpper() == Constants.CONST_NENHUM_VALUE)
                    .When(x => !string.IsNullOrWhiteSpace(x.Result))
                    .WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.ResultDate)
                .NotNull()
                .When(x => !string.IsNullOrWhiteSpace(x.Result) && x.ResultBool.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.ResultDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => !string.IsNullOrWhiteSpace(x.Result) && x.ResultBool.HasValue)
                .WithMessage(localizer["TestResultDateCannotBeHiggerThenCurrentDate"]);

            RuleFor(x => x.ResultDate)
                .GreaterThanOrEqualTo(x => x.TestDate)
                .When(x => !string.IsNullOrWhiteSpace(x.Result) && x.ResultBool.HasValue)
                .WithMessage(localizer["TestResultDateCannotBeEqualOrLessThenTestDate"]);

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
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationUniqueDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationUniqueDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationUniqueDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjUniqueDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneUniqueDose) && !x.HealthUnityCodeUniqueDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjUniqueDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose))
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityIneUniqueDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose) && !x.HealthUnityCodeUniqueDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCodeUniqueDose)
                .NotNull()
                .When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneUniqueDose))
                .WithMessage(localizer["FieldRequired"]);

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
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationFirstDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationFirstDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationFirstDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjFirstDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneFirstDose) && !x.HealthUnityCodeFirstDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjFirstDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose))
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityIneFirstDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose) && !x.HealthUnityCodeFirstDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCodeFirstDose)
                .NotNull()
                .When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneFirstDose))
                .WithMessage(localizer["FieldRequired"]);

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
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationSecondDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$"))
                .Must(x => CpfUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationSecondDose))
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjSecondDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneSecondDose) && !x.HealthUnityCodeSecondDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjSecondDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose))
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityIneSecondDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose) && !x.HealthUnityCodeSecondDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCodeSecondDose)
                .NotNull()
                .When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneSecondDose))
                .WithMessage(localizer["FieldRequired"]);

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
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationThirdDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationThirdDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationThirdDose)).WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjThirdDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineThirdDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneThirdDose) && !x.HealthUnityCodeThirdDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjThirdDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjThirdDose))
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityIneThirdDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineThirdDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjThirdDose) && !x.HealthUnityCodeThirdDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCodeThirdDose)
                .NotNull()
                .When(x => x.HasVaccineThirdDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjThirdDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneThirdDose))
                .WithMessage(localizer["FieldRequired"]);
        }
    }
}
