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
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccinePeriodType"]));

            RuleFor(x => x.MinTimeNextDose)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineMinTimeNextDose"]));

            RuleFor(x => x.MaxTimeNextDose)
                .NotNull()
                .When(x => x.PeriodType == EVaccinePeriodType.Variable)
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineMaxTimeNextDose"]));

            RuleFor(x => x.RequiredDoses)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineRequiredDoses"]));
        }
    }
}
