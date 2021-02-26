using FluentValidation;
using iPassport.Api.Models.Requests;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Plans
{
    public class ResendPinRequestValidator : AbstractValidator<ResendPinRequest>
    {
        public ResendPinRequestValidator()
        {
            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop)
               .NotEmpty()
                   .WithMessage("O número de telefone informado não é válido. Por favor, verifique")
               .Length(13)
                   .WithMessage("O número de telefone informado não é válido. Por favor, verifique")
               .Must(y => y.Substring(4, 1).Equals("9"))
                   .WithMessage("A informação inserida para o nono dígito não é válida.");

            RuleFor(x => x.Phone).Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage("O número de telefone informado não é válido. Por favor, verifique");

            RuleFor(s => s.UserId)
                .SetValidator(new GuidValidator("UserId"));
        }
    }
}
