using Amazon.Auth.AccessControlPolicy;
using FluentValidation;
using iPassport.Api.Models.Requests.Vaccine;
using iPassport.Domain.Enums;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Vaccines
{
    /// <summary>
    /// Age Group Vaccine Create Request Validator
    /// </summary>
    public class AgeGroupVaccineCreateRequestValidator : AbstractValidator<AgeGroupVaccineCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">Resource</param>
        public AgeGroupVaccineCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.PeriodType)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccinePeriodType"]));

            RuleFor(x => x.RequiredDoses)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineRequiredDoses"]));

            RuleFor(x => x.TimeNextDoseMax)
                .NotNull()
                .When(x => x.RequiredDoses > 1 && x.PeriodType == EVaccinePeriodType.Variable)
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineMinTimeNextDose"]));

            RuleFor(x => x.TimeNextDoseMin)
                .NotNull()
                .When(x => x.RequiredDoses > 1)
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineMaxTimeNextDose"]));


            RuleFor(x => x.AgeGroupInital)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineInitalAgeGroup"]));

            RuleFor(x => x.AgeGroupFinal)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VaccineFinalAgeGroup"]));
        }
    }
}
