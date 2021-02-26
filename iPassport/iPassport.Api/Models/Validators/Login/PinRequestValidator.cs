using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Domain.Enums;
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
                    .SetValidator(new CnsValidator("Document"));
            });

            When(x => x.Doctype == EDocumentType.CPF, () =>
            {
                RuleFor(x => x.Document)
                    .SetValidator(new CpfValidator("Document"));
            });

            When(x => x.Doctype == EDocumentType.Passport, () =>
            {
                RuleFor(x => x.Document)
                    .SetValidator(new PassportValidator("Document"));
            });

            When(x => x.Doctype == EDocumentType.RG, () =>
            {
                RuleFor(x => x.Document)
                    .SetValidator(new RgValidator("Document"));
            });

            When(x => x.Doctype == EDocumentType.InternationalDocument, () =>
            {
                RuleFor(x => x.Document)
                     .Length(1, 15).WithMessage("O campo Document não é um Cod. de Identificação Valido")
                    .Must(y => Regex.IsMatch(y, "^[1-9a-zA-Z]+$")).WithMessage("O campo Document não é um Cod. de Identificação Valido");
            });

            RuleFor(s => s.Phone)
                .SetValidator(new PhoneNumberValidator());

            RuleFor(x => x.Doctype)
                .IsInEnum().WithMessage("O campo doctype está inválido");

        }
    }
}
