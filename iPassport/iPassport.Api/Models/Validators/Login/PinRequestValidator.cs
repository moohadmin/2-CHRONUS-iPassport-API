using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Plans
{
    public class PinRequestValidator : AbstractValidator<PinRequest>
    {
        public PinRequestValidator()
        {
            RuleFor(s => s.Document)
                .SetValidator(new RequiredFieldValidator<string>("document"));

            RuleFor(s => s.Phone)
                .SetValidator(new RequiredFieldValidator<string>("Phone"));

            RuleFor(x => x.Doctype)
                .IsInEnum().WithMessage("O campo doctype está inválido");

        }
    }
}
