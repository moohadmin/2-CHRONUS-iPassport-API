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
                .NotEmpty()
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.Birthday)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(localizer["BirthdayCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.Cpf)
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

            RuleFor(x => x.Cns)
                .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.Cpf))
                .WithMessage(localizer["FieldRequired"]);
            
            RuleFor(x => x.Cns)
                .Length(15)
                .When(x => !string.IsNullOrWhiteSpace(x.Cns))
                .WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 15));

            RuleFor(x => x.RG)
                .MaximumLength(15)
                .When(x => !string.IsNullOrWhiteSpace(x.RG))
                .WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 15));

            RuleFor(x => x.PassportDoc)
                .MinimumLength(3)
                .When(x => !string.IsNullOrWhiteSpace(x.PassportDoc))
                .WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigitsBetween"], 3, 15));

            RuleFor(x => x.PassportDoc)
                .MaximumLength(15)
                .When(x => !string.IsNullOrWhiteSpace(x.PassportDoc))
                .WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigitsBetween"], 3, 15));

            RuleFor(x => x.InternationalDocument)
                .MaximumLength(15)
                .When(x => !string.IsNullOrWhiteSpace(x.InternationalDocument))
                .WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 15));

            RuleFor(x => x.PhoneNumber)
                .Must(x => x.ToString().Length >= 10 && x.ToString().Length <= 11)
                .When(x => x.PhoneNumber != null)
                .WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.Cep)
                .Length(8)
                .When(x => !string.IsNullOrWhiteSpace(x.Cep))
                .WithMessage(string.Format(localizer["FieldMustHaveANumberOfDigits"], 8));

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.State)
                .NotEmpty()
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.WasCovidInfected).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(localizer["FieldRequired"])
                .Must(x => x.ToUpper() == Constants.CONST_SIM_VALUE || x.ToUpper() == Constants.CONST_NAO_VALUE || x.ToUpper() == Constants.CONST_NENHUM_VALUE)
                .WithMessage(localizer["NonstandardField"]);

            RuleFor(x => x.WasTestPerformed)
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

            RuleFor(x => x.Result)
                .NotEmpty()
                .When(x => !string.IsNullOrWhiteSpace(x.WasTestPerformed) && x.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.Result)
                .Must(x => x.ToUpper() == Constants.CONST_POSITIVO_VALUE || x.ToUpper() == Constants.CONST_NEGATIVO_VALUE || x.ToUpper() == Constants.CONST_NENHUM_VALUE)
                .When(x => !string.IsNullOrWhiteSpace(x.Result))
                .WithMessage(localizer["NonstandardField"]);

            When(x => !string.IsNullOrWhiteSpace(x.Result) && x.ResultBool.HasValue, () =>
            {
                RuleFor(x => x.ResultDate).Cascade(CascadeMode.Stop)
                    .NotNull()
                    .WithMessage(localizer["FieldRequired"])
                    .Must(x => x.Value.Date <= DateTime.UtcNow.Date)
                    .WithMessage(localizer["TestResultDateCannotBeHiggerThenCurrentDate"]);
            });

            RuleFor(x => x.ResultDate)
                .GreaterThan(x => x.TestDate)
                .When(x => !string.IsNullOrWhiteSpace(x.Result) && x.ResultBool.HasValue && x.TestDate.HasValue)
                .WithMessage(localizer["TestResultDateCannotBeEqualOrLessThenTestDate"]);

            // VACCINE UNIQUE DOSE

            RuleFor(x => x.VaccineNameUniqueDose)
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameUniqueDose)
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchUniqueDose)
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateUniqueDose)
                .NotNull()
                .When(x => x.HasVaccineUniqueDoseData)
                .WithMessage(localizer["FieldRequired"]);
            
            RuleFor(x => x.VaccinationDateUniqueDose)
                .Must(x => x.Value.Date <= DateTime.UtcNow.Date)
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
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneUniqueDose) && !x.HealthUnityCodeUniqueDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjUniqueDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose) && x.HasVaccineUniqueDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityIneUniqueDose)
                .NotEmpty()
                .When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose) && !x.HealthUnityCodeUniqueDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCodeUniqueDose)
                .NotNull()
                .When(x => x.HasVaccineUniqueDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjUniqueDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneUniqueDose))
                .WithMessage(localizer["FieldRequired"]);

            // VACCINE FIRST DOSE

            RuleFor(x => x.VaccineNameFirstDose)
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameFirstDose)
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchFirstDose)
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateFirstDose)
                .NotNull()
                .When(x => x.HasVaccineFirstDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateFirstDose)
                .Must(x => x.Value.Date <= DateTime.UtcNow.Date)
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
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneFirstDose) && !x.HealthUnityCodeFirstDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjFirstDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose) && x.HasVaccineFirstDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityIneFirstDose)
                .NotEmpty()
                .When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose) && !x.HealthUnityCodeFirstDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCodeFirstDose)
                .NotNull()
                .When(x => x.HasVaccineFirstDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjFirstDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneFirstDose))
                .WithMessage(localizer["FieldRequired"]);

            // VACCINE SECOND DOSE

            RuleFor(x => x.VaccineNameSecondDose)
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameSecondDose)
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchSecondDose)
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateSecondDose)
                .NotNull()
                .When(x => x.HasVaccineSecondDoseData)
                .WithMessage(localizer["FieldRequired"]);
            
            RuleFor(x => x.VaccinationDateSecondDose)
                .Must(x => x.Value.Date <= DateTime.UtcNow.Date)
                .When(x => x.HasVaccineSecondDoseData)
                .WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.EmployeeCpfVaccinationSecondDose)
                .Must(x => Regex.IsMatch(x, "^[0-9]+$"))
                .Must(x => CpfUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpfVaccinationSecondDose) && x.HasVaccineSecondDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityCnpjSecondDose)
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneSecondDose) && !x.HealthUnityCodeSecondDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjSecondDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose) && x.HasVaccineSecondDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityIneSecondDose)
                .NotEmpty()
                .When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose) && !x.HealthUnityCodeSecondDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCodeSecondDose)
                .NotNull()
                .When(x => x.HasVaccineSecondDoseData && string.IsNullOrWhiteSpace(x.HealthUnityCnpjSecondDose) && string.IsNullOrWhiteSpace(x.HealthUnityIneSecondDose))
                .WithMessage(localizer["FieldRequired"]);

            // VACCINE THIRD DOSE

            RuleFor(x => x.VaccineNameThirdDose)
                .NotEmpty()
                .When(x => x.HasVaccineThirdDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccineManufacturerNameThirdDose)
                .NotEmpty()
                .When(x => x.HasVaccineThirdDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.BatchThirdDose)
                .NotEmpty()
                .When(x => x.HasVaccineThirdDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateThirdDose)
                .NotNull()
                .When(x => x.HasVaccineThirdDoseData)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.VaccinationDateThirdDose)
                .Must(x => x.Value.Date <= DateTime.UtcNow.Date)
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
                .NotEmpty()
                .When(x => x.HasVaccineThirdDoseData && string.IsNullOrWhiteSpace(x.HealthUnityIneThirdDose) && !x.HealthUnityCodeThirdDose.HasValue)
                .WithMessage(localizer["FieldRequired"]);

            RuleFor(x => x.HealthUnityCnpjThirdDose)
                .Must(x => CnpjUtils.Valid(x))
                .When(x => !string.IsNullOrWhiteSpace(x.HealthUnityCnpjThirdDose) && x.HasVaccineThirdDoseData)
                .WithMessage(string.Format(localizer["FieldValueInformedIsNotValidMasc"], localizer["ColumnNameImportFileCpf"]));

            RuleFor(x => x.HealthUnityIneThirdDose)
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
