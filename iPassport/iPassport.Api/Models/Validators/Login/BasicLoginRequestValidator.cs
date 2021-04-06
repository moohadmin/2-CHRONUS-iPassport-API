using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Basic Login Request Validator
    /// </summary>
    public class BasicLoginRequestValidator : AbstractValidator<BasicLoginRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public BasicLoginRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Username)
                .SetValidator(new RequiredFieldValidator<string>("Username", localizer));

            RuleFor(s => s.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password", localizer));
        }
    }
}
