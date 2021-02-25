using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Plans
{
    public class BasicLoginRequestValidator : AbstractValidator<BasicLoginRequest>
    {
        public BasicLoginRequestValidator()
        {
            RuleFor(s => s.Username)
                .SetValidator(new RequiredFieldValidator<string>("Username"));

            RuleFor(s => s.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password"));
        }
    }
}
