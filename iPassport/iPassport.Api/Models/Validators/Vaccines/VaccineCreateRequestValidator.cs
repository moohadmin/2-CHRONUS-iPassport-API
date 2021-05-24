using Amazon.Auth.AccessControlPolicy;
using FluentValidation;
using iPassport.Api.Models.Requests.Vaccine;
using iPassport.Domain.Enums;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace iPassport.Api.Models.Validators.Vaccines
{
    /// <summary>
    /// Vaccine Create Request Validator
    /// </summary>
    public class VaccineCreateRequestValidator : AbstractValidator<VaccineCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">Resource</param>
        public VaccineCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineName"]));

            RuleFor(x => x.Manufacturer)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineManufacturer"]));

            RuleFor(x => x.Diseases)
                .Must(x => x != null && x.Any())
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Diseases"]));

            RuleFor(x => x.ExpirationTimeInMonths)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineExpirationTimeInMonths"]));

            RuleFor(x => x.ImmunizationTimeInDays)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineImmunizationTimeInDays"]));

            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["IsActive"]));

            RuleFor(x => x.DosageType)
                .Must( x => x != null && x > 0)
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineDosageType"]));

            RuleFor(x => x.GeneralGroupVaccine)
                .SetValidator(new GeneralGroupVaccineCreateRequestValidator(localizer))
                .When(x => x.DosageType == EVaccineDosageType.GeneralGroup);

            RuleForEach(x => x.AgeGroupVaccines)
                .SetValidator(new AgeGroupVaccineCreateRequestValidator(localizer))
                .When(x => x.DosageType == EVaccineDosageType.AgeGroup);
        }
    }
}
