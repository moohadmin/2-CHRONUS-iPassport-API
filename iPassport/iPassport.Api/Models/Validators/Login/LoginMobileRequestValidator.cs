using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Plans
{
    public class LoginMobileRequestValidator : AbstractValidator<LoginMobileRequest>
    {
        public LoginMobileRequestValidator()
        {
            RuleFor(s => s.Pin)
                .NotNull().WithMessage("O campo Pin é obrigatório")
                .GreaterThanOrEqualTo(0).WithMessage("Código de autenticação inválido. Favor conferir código enviado.")
                .LessThanOrEqualTo(9999).WithMessage("Código de autenticação inválido. Favor conferir código enviado.");

            RuleFor(s => s.UserId)
                .SetValidator(new GuidValidator("UserId"));

            RuleFor(s => s.AcceptTerms)
                .SetValidator(new RequiredFieldValidator<bool>("AcceptTerms"));
        }
    }
}
