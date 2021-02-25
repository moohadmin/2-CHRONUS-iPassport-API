using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Login
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(s => s.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password"));

            RuleFor(s => s.PasswordConfirm)
                .SetValidator(new RequiredFieldValidator<string>("PasswordConfirm"));
        }
    }
}
