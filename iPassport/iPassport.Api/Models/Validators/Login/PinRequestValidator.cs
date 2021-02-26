using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Domain.Enums;
using iPassport.Domain.Utils;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Plans
{
    public class PinRequestValidator : AbstractValidator<PinRequest>
    {
        public PinRequestValidator()
        {
            RuleFor(s => s.Document)
                .SetValidator(new RequiredFieldValidator<string>("document"));

            When(x => x.Doctype == EDocumentType.CNS, () =>
            {
                RuleFor(x => x.Document)
                    .Length(15).WithMessage("o campo Document não é um CNS Valido")
                    .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                        .WithMessage("O campo Document não é um CNS Valido");
            });

            When(x => x.Doctype == EDocumentType.CPF, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                    .Length(11).WithMessage($"O campo Document não é um CPF Valido")
                    .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage($"O campo Document não é um CPF Valido")
                    .Must(x => CpfVerification.Validar(x))
                        .WithMessage($"O campo Document não é um CPF Valido");
            });

            When(x => x.Doctype == EDocumentType.Passport, () =>
            {
                RuleFor(x => x.Document)
                    .Length(3, 15).WithMessage("O campo Document não é um Passaporte Valido")
                    .Must(y => Regex.IsMatch(y, "^[a-zA-Z]{2}[0-9]+$"))
                        .WithMessage("O campo Document não é um Passaporte Valido");
            });

            When(x => x.Doctype == EDocumentType.RG, () =>
            {
                RuleFor(x => x.Document)
                    .Length(1, 15).WithMessage("O campo Document não é um RG Valido")
                    .Must(y => Regex.IsMatch(y, "^[0-9a-zA-Z]+$"))
                        .WithMessage($"O campo Document não é um RG Valido");
            });

            When(x => x.Doctype == EDocumentType.InternationalDocument, () =>
            {
                RuleFor(x => x.Document)
                     .Length(1, 15).WithMessage("O campo Document não é um Cod. de Identificação Valido")
                    .Must(y => Regex.IsMatch(y, "^[1-9a-zA-Z]+$"))
                        .WithMessage("O campo Document não é um Cod. de Identificação Valido");
            });


            RuleFor(s => s.Phone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("O número de telefone informado não é válido. Por favor, verifique")
                .Length(13)
                    .WithMessage("O número de telefone informado não é válido. Por favor, verifique")
                .Must(y => y.Substring(4, 1).Equals("9"))
                    .WithMessage("A informação inserida para o nono dígito não é válida.");

            RuleFor(x => x.Phone).Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage("O número de telefone informado não é válido. Por favor, verifique");


            RuleFor(x => x.Doctype)
                .IsInEnum().WithMessage("O campo doctype está inválido");
        }
    }
}
