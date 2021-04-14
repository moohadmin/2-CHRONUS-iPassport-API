using FluentValidation;
using iPassport.Api.Models.Requests.Company;
using iPassport.Api.Models.Validators.Plans;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Company
{
    /// <summary>
    /// Company Responsible Edit Request Validator
    /// </summary>
    public class CompanyEditRequestValidator : AbstractValidator<CompanyEditRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">String localizer</param>
        public CompanyEditRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], "Id"));

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
                .SetValidator(new AddressEditValidator(localizer))
                .When(x => x.Address != null);

            RuleFor(x => x.SegmentId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Segment"]));

            RuleFor(s => s.Responsible)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Responsible"]));
            
            RuleFor(s => s.Responsible)
                .SetValidator(new CompanyResponsibleEditRequestValidator(localizer))
                .When(x => x.Responsible != null);

            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["IsActive"]));
        }
    }
}
