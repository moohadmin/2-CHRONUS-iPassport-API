using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Login
{
    /// <summary>
    /// Reset Password Request Validator
    /// </summary>
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public ResetPasswordRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password", localizer));

            RuleFor(s => s.PasswordConfirm)
                .SetValidator(new RequiredFieldValidator<string>("PasswordConfirm", localizer));
        }
    }
}
