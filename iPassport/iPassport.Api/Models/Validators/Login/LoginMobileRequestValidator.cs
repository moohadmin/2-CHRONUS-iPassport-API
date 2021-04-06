using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Login Mobile Request Validator
    /// </summary>
    public class LoginMobileRequestValidator : AbstractValidator<LoginMobileRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public LoginMobileRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Pin)
                .SetValidator(new RequiredFieldValidator<int>("Pin", localizer))
                .GreaterThanOrEqualTo(0).WithMessage(localizer["InvalidPin"])
                .LessThanOrEqualTo(9999).WithMessage(localizer["InvalidPin"]);

            RuleFor(s => s.UserId)
                .SetValidator(new GuidValidator("UserId", localizer));

            RuleFor(s => s.AcceptTerms)
                .SetValidator(new RequiredFieldValidator<bool>("AcceptTerms", localizer));
        }
    }
}
