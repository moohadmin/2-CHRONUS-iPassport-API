using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Plans
{
    public class ResendPinRequestValidator : AbstractValidator<ResendPinRequest>
    {
        public ResendPinRequestValidator()
        {
            RuleFor(s => s.Phone)
                .SetValidator(new PhoneNumberBrazilianValidator());

            RuleFor(s => s.UserId)
                .SetValidator(new GuidValidator("UserId"));
        }
    }
}
