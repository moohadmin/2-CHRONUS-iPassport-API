using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Pin Request Validator
    /// </summary>
    public class PinRequestValidator : AbstractValidator<PinRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public PinRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Document)
                .SetValidator(new RequiredFieldValidator<string>("document", localizer));

            When(x => x.Doctype == EDocumentType.CNS, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                    .Length(15).WithMessage(string.Format(localizer["InvalidField"], localizer["Cns"]))
                    .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                        .WithMessage(string.Format(localizer["InvalidField"], localizer["Cns"]));
            });

            When(x => x.Doctype == EDocumentType.CPF, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                    .Length(11).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                    .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage(string.Format(localizer["InvalidField"], localizer["Cpf"]))
                    .Must(x => CpfUtils.Valid(x))
                        .WithMessage(string.Format(localizer["InvalidField"], localizer["Cpf"]));
            });

            When(x => x.Doctype == EDocumentType.Passport, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                    .Length(3, 15).WithMessage(string.Format(localizer["InvalidField"], localizer["PassportDocument"]))
                    .Must(y => Regex.IsMatch(y, "^[a-zA-Z]{2}[0-9]+$"))
                        .WithMessage(string.Format(localizer["InvalidField"], localizer["PassportDocument"]));
            });

            When(x => x.Doctype == EDocumentType.RG, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                    .Length(1, 15).WithMessage(string.Format(localizer["InvalidField"], localizer["Rg"]))
                    .Must(y => Regex.IsMatch(y, "^[0-9a-zA-Z]+$"))
                        .WithMessage(string.Format(localizer["InvalidField"], localizer["Rg"]));
            });

            When(x => x.Doctype == EDocumentType.InternationalDocument, () =>
            {
                RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                     .Length(1, 15).WithMessage(string.Format(localizer["InvalidField"], localizer["InternationalDocument"]))
                    .Must(y => Regex.IsMatch(y, "^[0-9a-zA-Z]+$"))
                        .WithMessage(string.Format(localizer["InvalidField"], localizer["InternationalDocument"]));
            });

            RuleFor(s => s.Phone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(string.Format(localizer["RequiredField"], localizer["Telephone"]))                
                .Must(y => PhoneNumberUtils.ValidMobile(y))
                    .WithMessage(string.Format(localizer["InvalidField"], localizer["Telephone"]));

            RuleFor(x => x.Doctype)
                .IsInEnum().WithMessage(string.Format(localizer["InvalidField"], localizer["Doctype"]));
        }
    }
}
