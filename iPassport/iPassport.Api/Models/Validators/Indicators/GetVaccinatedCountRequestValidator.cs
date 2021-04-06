using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System;

namespace iPassport.Api.Models.Validators.Indicators
{
    /// <summary>
    /// Get Vaccinated Count Request Validator
    /// </summary>
    public class GetVaccinatedCountRequestValidator : AbstractValidator<GetVaccinatedCountRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public GetVaccinatedCountRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.StartTime)
                .SetValidator(new RequiredFieldValidator<DateTime>("StartTime", localizer));

            RuleFor(s => s.EndTime)
                .SetValidator(new RequiredFieldValidator<DateTime>("EndTime", localizer));

            RuleFor(s => s.EndTime)
                .SetValidator(new RequiredFieldValidator<DateTime>("EndTime", localizer));

            RuleFor(s => s.DosageCount)
                .Must(s => s >= 0)
                .WithMessage(string.Format(localizer["RequiredField"], "DosageCount"));
        }
    }
}
