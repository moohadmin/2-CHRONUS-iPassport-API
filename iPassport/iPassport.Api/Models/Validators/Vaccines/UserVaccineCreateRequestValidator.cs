using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System;

namespace iPassport.Api.Models.Validators.Vaccines
{
    /// <summary>
    /// User Vaccine Create Request Validator
    /// </summary>
    public class UserVaccineCreateRequestValidator : AbstractValidator<UserVaccineCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">localizer</param>
        public UserVaccineCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Dose)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "Dose"));

            RuleFor(x => x.VaccinationDate)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "VaccinationDate"))
                .LessThanOrEqualTo(DateTime.UtcNow).When(x => x.VaccinationDate.HasValue).WithMessage(localizer["VaccinationDateValidation"]);

            RuleFor(x => x.Vaccine)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "Vaccine"));

            RuleFor(x => x.Batch)
                .SetValidator(new RequiredFieldValidator<string>("Batch", localizer));

            RuleFor(x => x.HealthUnitId)
                .SetValidator(new GuidValidator("HealthUnitId", localizer));
        }
    }
}
