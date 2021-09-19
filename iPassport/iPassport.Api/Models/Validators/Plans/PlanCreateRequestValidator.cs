using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Plan Create Request Validator
    /// </summary>
    public class PlanCreateRequestValidator : AbstractValidator<PlanCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public PlanCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Type)
                .SetValidator(new RequiredFieldValidator<string>("Type", localizer));

            RuleFor(s => s.Description)
                .SetValidator(new RequiredFieldValidator<string>("Description", localizer));

            RuleFor(s => s.ColorStart)
                .SetValidator(new RequiredFieldValidator<string>("ColorStart", localizer))
                .Must(x => Regex.IsMatch(x, "^([A-Fa-f0-9]{6})$")).WithMessage(string.Format(localizer["InvalidField"], "ColorStart")); 

            RuleFor(s => s.ColorEnd)
                .SetValidator(new RequiredFieldValidator<string>("ColorEnd", localizer))
                .Must(x => Regex.IsMatch(x, "^([A-Fa-f0-9]{6})$")).WithMessage(string.Format(localizer["InvalidField"], "ColorEnd")); 

            RuleFor(s => s.Active)
                .NotNull().WithMessage(string.Format(localizer["RequiredField"], "Active"));
                
        }
    }
}
