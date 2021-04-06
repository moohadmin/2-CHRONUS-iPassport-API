using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Plans
{
    public class PinRequestValidator : AbstractValidator<PinRequest>
    {
        public PinRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Document)
                .SetValidator(new RequiredFieldValidator<string>("document", localizer));

            When(x => x.Doctype == EDocumentType.CNS, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                    .Length(15).WithMessage(string.Format(localizer["InvalidField"], "Document"))
                    .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                        .WithMessage(string.Format(localizer["InvalidField"], "Document"));
            });

            When(x => x.Doctype == EDocumentType.CPF, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                    .Length(11).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                    .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                    .Must(x => CpfUtils.Valid(x))
                        .WithMessage(string.Format(localizer["InvalidField"], "CPF"));
            });

            When(x => x.Doctype == EDocumentType.Passport, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                    .Length(3, 15).WithMessage(string.Format(localizer["InvalidField"], "Document"))
                    .Must(y => Regex.IsMatch(y, "^[a-zA-Z]{2}[0-9]+$"))
                        .WithMessage(string.Format(localizer["InvalidField"], "Document"));
            });

            When(x => x.Doctype == EDocumentType.RG, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                    .Length(1, 15).WithMessage(string.Format(localizer["InvalidField"], "RG"))
                    .Must(y => Regex.IsMatch(y, "^[0-9a-zA-Z]+$"))
                        .WithMessage(string.Format(localizer["InvalidField"], "RG"));
            });

            When(x => x.Doctype == EDocumentType.InternationalDocument, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                     .Length(1, 15).WithMessage(string.Format(localizer["InvalidField"], "Document"))
                    .Must(y => Regex.IsMatch(y, "^[0-9a-zA-Z]+$"))
                        .WithMessage(string.Format(localizer["InvalidField"], "Document"));
            });

            RuleFor(s => s.Phone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(string.Format(localizer["InvalidField"], localizer["Telephone"]))
                .Must(y => {
                        return !y.StartsWith("55") || (y.Substring(4, 1).Equals("9") && y.Length == 13); 
                        })
                    .WithMessage(string.Format(localizer["InvalidField"], localizer["Telephone"]))
                .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                    .WithMessage(string.Format(localizer["InvalidField"], localizer["Telephone"]));

            RuleFor(x => x.Doctype)
                .IsInEnum().WithMessage(string.Format(localizer["InvalidField"], "Doctype"));
        }
    }
}
