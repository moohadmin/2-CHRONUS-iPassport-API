using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Plans
{
    public class LoginMobileRequestValidator : AbstractValidator<LoginMobileRequest>
    {
        public LoginMobileRequestValidator()
        {
            RuleFor(s => s.Pin)
                .NotNull()
                .WithMessage("O campo Pin é obrigatório");

            RuleFor(s => s.UserId)
                .SetValidator(new GuidValidator("UserId"));
        }
    }
}
