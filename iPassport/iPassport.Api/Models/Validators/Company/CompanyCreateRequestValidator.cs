using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Company Create Request Validator
    /// </summary>
    public class CompanyCreateRequestValidator : AbstractValidator<CompanyCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">String localizer</param>
        public CompanyCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Name"]));

            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], "CNPJ"));
            
            RuleFor(x => x.Cnpj)
                 .Must(x => Regex.IsMatch(x, "^[0-9]{14}$")).WithMessage(string.Format(localizer["InvalidField"], "CNPJ"))
                 .Must(x => CnpjUtils.Valid(x)).WithMessage(string.Format(localizer["InvalidField"], "Cnpj"))
                 .When(y => !string.IsNullOrWhiteSpace(y.Cnpj));

            RuleFor(s => s.Address)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Address"]));
            RuleFor(s => s.Address)
                .SetValidator(new AddressValidator(localizer))
                .When(x => x.Address != null);

            RuleFor(x => x.SegmentId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Segment"]));

            RuleFor(s => s.Responsible)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Responsible"]));
            RuleFor(s => s.Responsible)
                .SetValidator(new CompanyResponsibleRequestValidator(localizer))
                .When(x => x.Responsible != null);

            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["IsActive"]));
        }
    }
}
