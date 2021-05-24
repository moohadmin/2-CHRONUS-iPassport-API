using Amazon.Auth.AccessControlPolicy;
using FluentValidation;
using iPassport.Api.Models.Requests.Vaccine;
using iPassport.Domain.Enums;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Vaccines
{
    /// <summary>
    /// General Group Vaccine Create Request Validator
    /// </summary>
    public class GeneralGroupVaccineCreateRequestValidator : AbstractValidator<GeneralGroupVaccineCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">Resource</param>
        public GeneralGroupVaccineCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.PeriodType)
                .Must(x => x != null && x > 0)
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccinePeriodType"]));

            RuleFor(x => x.RequiredDoses)
               .NotNull()
               .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineRequiredDoses"]));

            RuleFor(x => x.TimeNextDoseMin)
                .NotNull()
                .When(x => x.RequiredDoses > 1)
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineMaxTimeNextDose"]));

            RuleFor(x => x.TimeNextDoseMax)
               .NotNull()
               .When(x => x.RequiredDoses > 1 && x.PeriodType == EVaccinePeriodType.Variable)
               .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineMinTimeNextDose"]));
           
        }
    }
}
