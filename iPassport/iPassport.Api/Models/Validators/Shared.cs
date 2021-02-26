using FluentValidation;
using System;

namespace iPassport.Api.Models.Validators
{
    public class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator() { }
        public GuidValidator(string fieldName)
        {
            RuleFor(x => x)
                .SetValidator(new RequiredFieldValidator<Guid>(fieldName))
                .Must(g => Guid.TryParse(g.ToString(), out g)).WithMessage("Identificador inválido");
        }
    }

    public class RequiredFieldValidator<T> : AbstractValidator<T>
    {
        public RequiredFieldValidator(string fieldName)
        {
            RuleFor(x => x)
                .NotNull().WithMessage($"O campo {fieldName} é obrigatório")
                .NotEmpty().WithMessage($"O campo {fieldName} é obrigatório");
        }
    }
}
