using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Login
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password", localizer));

            RuleFor(s => s.PasswordConfirm)
                .SetValidator(new RequiredFieldValidator<string>("PasswordConfirm", localizer));
        }
    }
}
