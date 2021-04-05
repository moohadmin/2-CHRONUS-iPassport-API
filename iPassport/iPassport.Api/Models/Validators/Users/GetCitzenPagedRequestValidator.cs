using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Users
{
    /// <summary>
    /// Get Citzen Paged Request Validator Class
    /// </summary>
    public class GetCitzenPagedRequestValidator : AbstractValidator<GetCitzenPagedRequest>
    {
        /// <summary>
        /// Get Citzen Paged Request Validator constructor
        /// </summary>
        /// <param name="localizer">resource</param>
        public GetCitzenPagedRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Initials)
                .Must(x => string.IsNullOrWhiteSpace(x) || x.Length >= 3).WithMessage(string.Format(localizer["InitalsRequestMin"], "3"))
                .SetValidator(new RequiredFieldValidator<string>("Initials", localizer));

            RuleFor(x => x.PageNumber)
                .SetValidator(new RequiredFieldValidator<int>(localizer["PageNumber"], localizer));

            RuleFor(x => x.PageSize)
                .SetValidator(new RequiredFieldValidator<int>(localizer["PageSize"], localizer));

            RuleFor(x => x.DocumentType)
                .IsInEnum().When(x => x.DocumentType.HasValue)
                .WithMessage(string.Format(localizer["InvalidField"], "DocumentType"));

            When(y => !string.IsNullOrWhiteSpace(y.Document) && y.DocumentType.HasValue, () =>
            {

                When(x => x.DocumentType == EDocumentType.CNS, () =>
                {
                    RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                        .Length(15).WithMessage(string.Format(localizer["InvalidField"], "CNS"))
                        .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                            .WithMessage(string.Format(localizer["InvalidField"], "CNS"));
                });

                When(x => x.DocumentType == EDocumentType.CPF, () =>
                {
                    RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                        .Length(11).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                        .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                        .Must(x => CpfUtils.Valid(x))
                            .WithMessage(string.Format(localizer["InvalidField"], "CPF"));
                });

                When(x => x.DocumentType == EDocumentType.Passport, () =>
                {
                    RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                        .Length(3, 15).WithMessage(string.Format(localizer["InvalidField"], "Passport"))
                        .Must(y => Regex.IsMatch(y, "^[a-zA-Z]{2}[0-9]+$"))
                            .WithMessage(string.Format(localizer["InvalidField"], "Passport"));
                });

                When(x => x.DocumentType == EDocumentType.RG, () =>
                {
                    RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                        .Length(1, 15).WithMessage(string.Format(localizer["InvalidField"], "RG"))
                        .Must(y => Regex.IsMatch(y, "^[0-9a-zA-Z]+$"))
                            .WithMessage(string.Format(localizer["InvalidField"], "RG"));
                });

                When(x => x.DocumentType == EDocumentType.InternationalDocument, () =>
                {
                    RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
                         .Length(1, 15).WithMessage(string.Format(localizer["InvalidField"], "InternationalDocument"))
                        .Must(y => Regex.IsMatch(y, "^[0-9a-zA-Z]+$"))
                            .WithMessage(string.Format(localizer["InvalidField"], "InternationalDocument"));
                });

            });
        }
    }
}
