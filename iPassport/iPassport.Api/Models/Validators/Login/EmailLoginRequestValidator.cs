using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Plans
{
    public class EmailLoginRequestValidator : AbstractValidator<EmailLoginRequest>
    {
        public EmailLoginRequestValidator()
        {
            RuleFor(s => s.Email)
                .SetValidator(new RequiredFieldValidator<string>("E-mail"))
                .EmailAddress().WithMessage("E-mail inválido");               

            RuleFor(s => s.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password"));
        }
    }
}
