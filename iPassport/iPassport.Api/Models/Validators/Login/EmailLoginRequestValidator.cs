using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Email Login Request Validator
    /// </summary>
    public class EmailLoginRequestValidator : AbstractValidator<EmailLoginRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public EmailLoginRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Email)
                .SetValidator(new RequiredFieldValidator<string>("E-mail", localizer))
                .EmailAddress().WithMessage(string.Format(localizer["InvalidField"], "E-mail"));               

            RuleFor(s => s.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password", localizer));
        }
    }
}
