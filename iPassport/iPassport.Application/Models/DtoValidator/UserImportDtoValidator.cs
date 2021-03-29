using FluentValidation;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Domain.Dtos.DtoValidator
{
    public class UserImportDtoValidator : AbstractValidator<UserImportDto>
    {
        public UserImportDtoValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.FullName)
                .NotNull().WithMessage(string.Format(localizer["FieldRequired"], "FullName"))
                .NotEmpty().WithMessage(string.Format(localizer["FieldRequired"], "FullName"));

            RuleFor(x => x.Cpf)
                .NotNull().WithMessage(string.Format(localizer["FieldRequired"], "CPF"))
                .NotEmpty().WithMessage(string.Format(localizer["FieldRequired"], "CPF"));

            RuleFor(x => x.Cns)
                .NotNull().WithMessage(string.Format(localizer["FieldRequired"], "CNS"))
                .NotEmpty().WithMessage(string.Format(localizer["FieldRequired"], "CNS"));

            RuleFor(x => x.CountryCode)
                .NotNull().WithMessage(string.Format(localizer["FieldRequired"], "CountryCode"));

            RuleFor(x => x.PhoneNumber)
                .NotNull().WithMessage(string.Format(localizer["FieldRequired"], "PhoneNumber"));

            RuleFor(x => x.City)
                .NotNull().WithMessage(string.Format(localizer["FieldRequired"], "City"))
                .NotEmpty().WithMessage(string.Format(localizer["FieldRequired"], "City"));

            RuleFor(x => x.State)
                .NotNull().WithMessage(string.Format(localizer["FieldRequired"], "State"))
                .NotEmpty().WithMessage(string.Format(localizer["FieldRequired"], "State"));

            RuleFor(x => x.Country)
                .NotNull().WithMessage(string.Format(localizer["FieldRequired"], "Country"))
                .NotEmpty().WithMessage(string.Format(localizer["FieldRequired"], "Country"));

            // VACCINE UNIQUE DOSE

            RuleFor(x => x.VaccineNameUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineNameUniqueDose"))
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineNameUniqueDose"));

            RuleFor(x => x.VaccineManufacturerNameUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineManufacturerNameUniqueDose"))
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineManufacturerNameUniqueDose"));

            RuleFor(x => x.BatchUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData).WithMessage(string.Format(localizer["FieldRequired"], "BatchUniqueDose"))
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData).WithMessage(string.Format(localizer["FieldRequired"], "BatchUniqueDose"));

            RuleFor(x => x.VaccinationDateUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccinationDateUniqueDose"))
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccinationDateUniqueDose"));

            RuleFor(x => x.HealthUnityCnpjUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData && string.IsNullOrEmpty(x.HealthUnityIneUniqueDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityCnpjUniqueDose"))
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData && string.IsNullOrEmpty(x.HealthUnityIneUniqueDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityCnpjUniqueDose"));

            RuleFor(x => x.HealthUnityIneUniqueDose)
                .NotNull().When(x => x.HasVaccineUniqueDoseData && string.IsNullOrEmpty(x.HealthUnityCnpjUniqueDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityIneUniqueDose"))
                .NotEmpty().When(x => x.HasVaccineUniqueDoseData && string.IsNullOrEmpty(x.HealthUnityCnpjUniqueDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityIneUniqueDose"));

            // VACCINE FIRST DOSE

            RuleFor(x => x.VaccineNameFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineNameFirstDose"))
                .NotEmpty().When(x => x.HasVaccineFirstDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineNameFirstDose"));

            RuleFor(x => x.VaccineManufacturerNameFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineManufacturerNameFirstDose"))
                .NotEmpty().When(x => x.HasVaccineFirstDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineManufacturerNameFirstDose"));

            RuleFor(x => x.BatchFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData).WithMessage(string.Format(localizer["FieldRequired"], "BatchFirstDose"))
                .NotEmpty().When(x => x.HasVaccineFirstDoseData).WithMessage(string.Format(localizer["FieldRequired"], "BatchFirstDose"));

            RuleFor(x => x.VaccinationDateFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccinationDateFirstDose"))
                .NotEmpty().When(x => x.HasVaccineFirstDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccinationDateFirstDose"));

            RuleFor(x => x.HealthUnityCnpjFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData && string.IsNullOrEmpty(x.HealthUnityIneFirstDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityCnpjFirstDose"))
                .NotEmpty().When(x => x.HasVaccineFirstDoseData && string.IsNullOrEmpty(x.HealthUnityIneFirstDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityCnpjFirstDose"));

            RuleFor(x => x.HealthUnityIneFirstDose)
                .NotNull().When(x => x.HasVaccineFirstDoseData && string.IsNullOrEmpty(x.HealthUnityCnpjFirstDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityIneFirstDose"))
                .NotEmpty().When(x => x.HasVaccineFirstDoseData && string.IsNullOrEmpty(x.HealthUnityCnpjFirstDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityIneFirstDose"));

            // VACCINE SECOND DOSE

            RuleFor(x => x.VaccineNameSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineNameSecondDose"))
                .NotEmpty().When(x => x.HasVaccineSecondDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineNameSecondDose"));

            RuleFor(x => x.VaccineManufacturerNameSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineManufacturerNameSecondDose"))
                .NotEmpty().When(x => x.HasVaccineSecondDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineManufacturerNameSecondDose"));

            RuleFor(x => x.BatchSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData).WithMessage(string.Format(localizer["FieldRequired"], "BatchSecondDose"))
                .NotEmpty().When(x => x.HasVaccineSecondDoseData).WithMessage(string.Format(localizer["FieldRequired"], "BatchSecondDose"));

            RuleFor(x => x.VaccinationDateSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccinationDateSecondDose"))
                .NotEmpty().When(x => x.HasVaccineSecondDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccinationDateSecondDose"));

            RuleFor(x => x.HealthUnityCnpjSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData && string.IsNullOrEmpty(x.HealthUnityIneSecondDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityCnpjSecondDose"))
                .NotEmpty().When(x => x.HasVaccineSecondDoseData && string.IsNullOrEmpty(x.HealthUnityIneSecondDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityCnpjSecondDose"));

            RuleFor(x => x.HealthUnityIneSecondDose)
                .NotNull().When(x => x.HasVaccineSecondDoseData && string.IsNullOrEmpty(x.HealthUnityCnpjSecondDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityIneSecondDose"))
                .NotEmpty().When(x => x.HasVaccineSecondDoseData && string.IsNullOrEmpty(x.HealthUnityCnpjSecondDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityIneSecondDose"));

            // VACCINE THIRD DOSE

            RuleFor(x => x.VaccineNameThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineNameThirdDose"))
                .NotEmpty().When(x => x.HasVaccineThirdDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineNameThirdDose"));

            RuleFor(x => x.VaccineManufacturerNameThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineManufacturerNameThirdDose"))
                .NotEmpty().When(x => x.HasVaccineThirdDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccineManufacturerNameThirdDose"));

            RuleFor(x => x.BatchThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData).WithMessage(string.Format(localizer["FieldRequired"], "BatchThirdDose"))
                .NotEmpty().When(x => x.HasVaccineThirdDoseData).WithMessage(string.Format(localizer["FieldRequired"], "BatchThirdDose"));

            RuleFor(x => x.VaccinationDateThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccinationDateThirdDose"))
                .NotEmpty().When(x => x.HasVaccineThirdDoseData).WithMessage(string.Format(localizer["FieldRequired"], "VaccinationDateThirdDose"));

            RuleFor(x => x.HealthUnityCnpjThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData && string.IsNullOrEmpty(x.HealthUnityIneThirdDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityCnpjThirdDose"))
                .NotEmpty().When(x => x.HasVaccineThirdDoseData && string.IsNullOrEmpty(x.HealthUnityIneThirdDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityCnpjThirdDose"));

            RuleFor(x => x.HealthUnityIneThirdDose)
                .NotNull().When(x => x.HasVaccineThirdDoseData && string.IsNullOrEmpty(x.HealthUnityCnpjThirdDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityIneThirdDose"))
                .NotEmpty().When(x => x.HasVaccineThirdDoseData && string.IsNullOrEmpty(x.HealthUnityCnpjThirdDose)).WithMessage(string.Format(localizer["FieldRequired"], "HealthUnityIneThirdDose"));
        }
    }
}
