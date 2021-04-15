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
                .When(x => string.IsNullOrWhiteSpace(x.Cns))
                .WithMessage(localizer["FieldRequired"])
                .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.Cns))
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.Cpf).Cascade(CascadeMode.Stop)
                .Length(11)
                    .When(x => !string.IsNullOrWhiteSpace(x.Cpf))
                    .WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 8))
                .Must(x => Regex.IsMatch(x, "^[0-9]+$"))
                    .When(x => !string.IsNullOrWhiteSpace(x.Cpf))
                    .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]))
                .Must(x => CpfUtils.Valid(x))
                    .When(x => !string.IsNullOrWhiteSpace(x.Cpf))
                    .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.Cns).Cascade(CascadeMode.Stop)
                .NotNull()
                .When(x => string.IsNullOrWhiteSpace(x.Cpf))
                .WithMessage(localizer["FieldRequired"])
                .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.Cpf))
                .WithMessage(localizer["FieldRequired"]);
            
            RuleFor(x => x.Cns)
                .Length(15)
                .When(x => !string.IsNullOrWhiteSpace(x.Cns))
                .WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 15));

            RuleFor(x => x.PhoneNumber)
                .Must(x => x.ToString().Length >= 10 && x.ToString().Length <= 11)
                .WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage(string.Format(localizer["FieldInvalid"], localizer["ColumnNameImportFileEmail"]));

            RuleFor(x => x.Cep)
                .Length(8)
                .When(x => !string.IsNullOrWhiteSpace(x.Cep))
                .WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 8));

            RuleFor(x => x.City).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer["FieldRequired"])
                .NotEmpty()
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.State).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer["FieldRequired"])
                .NotEmpty()
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.Country).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer["FieldRequired"])
                .NotEmpty()
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.WasCovidInfected).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer["FieldRequired"])
                .NotEmpty()
                .WithMessage(localizer["FieldRequired"])
                .Must(x => x.ToUpper() == Constants.CONST_SIM_VALUE || x.ToUpper() == Constants.CONST_NAO_VALUE || x.ToUpper() == Constants.CONST_NENHUM_VALUE)
                .WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.WasTestPerformed).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer["FieldRequired"])
                .NotEmpty()
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.WasTestPerformed)
                .Must(x => x.ToUpper() == Constants.CONST_SIM_VALUE || x.ToUpper() == Constants.CONST_NAO_VALUE || x.ToUpper() == Constants.CONST_NENHUM_VALUE)
                .When(x => !string.IsNullOrWhiteSpace(x.WasTestPerformed))
                .WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.TestDate)
                .NotNull()
                .When(x => !string.IsNullOrWhiteSpace(x.WasTestPerformed) && x.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.TestDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.TestDate.HasValue)
                .WithMessage(localizer["TestDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.Result).Cascade(CascadeMode.Stop)
                .NotNull()
                .When(x => !string.IsNullOrWhiteSpace(x.WasTestPerformed) && x.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE)
                .WithMessage(localizer["FieldRequired"])
                .NotEmpty()
                .When(x => !string.IsNullOrWhiteSpace(x.WasTestPerformed) && x.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.Result)
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
                .GreaterThan(x => x.TestDate)
                .When(x => !string.IsNullOrWhiteSpace(x.Result) && x.ResultBool.HasValue && x.TestDate.HasValue)
                .WithMessage(localizer["TestResultDateCannotBeEqualOrLessThenTestDate"]);

            // VACCINE UNIQUE DOSE

            RuleFor(x => x.VaccineNameUniqueDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameUniqueDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchUniqueDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateUniqueDose)
                .NotNull()
                .When(x => x.HasVaccineUniqueDoseData)
                .WithMessage(localizer["FieldRequired"]);
            
            RuleFor(x => x.VaccinationDateUniqueDose)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.HasVaccineUniqueDoseData)
                .WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationUniqueDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$"))
                .When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationUniqueDose) && x.HasVaccineUniqueDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.EmployeeCpfVaccinationUniqueDose)
                .Must(x => CpfUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationUniqueDose) && x.HasVaccineUniqueDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjUniqueDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneUniqueDose) && !x.HealthUnityCodeUniqueDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjUniqueDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose) && x.HasVaccineUniqueDoseData)
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
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameFirstDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchFirstDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateFirstDose)
                .NotNull()
                .When(x => x.HasVaccineFirstDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateFirstDose)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.HasVaccineFirstDoseData)
                .WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationFirstDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$"))
                .When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationFirstDose) && x.HasVaccineFirstDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));
            
            RuleFor(x => x.EmployeeCpfVaccinationFirstDose)
                .Must(x => CpfUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationFirstDose) && x.HasVaccineFirstDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjFirstDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneFirstDose) && !x.HealthUnityCodeFirstDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjFirstDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose) && x.HasVaccineFirstDoseData)
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
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameSecondDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchSecondDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateSecondDose)
                .NotNull()
                .When(x => x.HasVaccineSecondDoseData)
                .WithMessage(localizer["FieldRequired"]);
            
            RuleFor(x => x.VaccinationDateSecondDose)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.HasVaccineSecondDoseData)
                .WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationSecondDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$"))
                .Must(x => CpfUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationSecondDose) && x.HasVaccineSecondDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjSecondDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneSecondDose) && !x.HealthUnityCodeSecondDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjSecondDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose) && x.HasVaccineSecondDoseData)
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
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineThirdDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameThirdDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineThirdDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchThirdDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineThirdDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateThirdDose)
                .NotNull()
                .When(x => x.HasVaccineThirdDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateThirdDose)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.HasVaccineThirdDoseData)
                .WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationThirdDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$"))
                .When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationThirdDose) && x.HasVaccineThirdDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.EmployeeCpfVaccinationThirdDose)
                .Must(x => CpfUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationThirdDose) && x.HasVaccineThirdDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjThirdDose)
                .NotNull()
                .NotEmpty()
                .When(x => x.HasVaccineThirdDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneThirdDose) && !x.HealthUnityCodeThirdDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjThirdDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjThirdDose) && x.HasVaccineThirdDoseData)
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
