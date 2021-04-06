using FluentValidation;
using iPassport.Api.Models.Requests.HealthUnit;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.HealthUnit
{
    /// <summary>
    /// Health Unit Edit Request Validator
    /// </summary>
    public class HealthUnitEditRequestValidator : AbstractValidator<HealthUnitEditRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public HealthUnitEditRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], "Id"));

            RuleFor(x => x.Name)
                .SetValidator(new RequiredFieldValidator<string>(localizer["Name"], localizer));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(string.Format(localizer["InvalidField"], localizer["Email"])).When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.IsActive)
                .SetValidator(new RequiredFieldValidator<bool?>(localizer["IsActive"], localizer));

            RuleFor(x => x.Address)
                .SetValidator(new AddressEditValidator(localizer));

            RuleFor(x => x.TypeId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Type"]));

            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Company"]));

            RuleFor(x => x.Cnpj)
                .Must(x => CnpjUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.Cnpj)).WithMessage(string.Format(localizer["InvalidField"], "CNPJ"));

            RuleFor(x => x.Ine)
                .MaximumLength(10).When(x => !string.IsNullOrWhiteSpace(x.Ine)).WithMessage(string.Format(localizer["InvalidField"], "INE"))
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.Ine)).WithMessage(string.Format(localizer["InvalidField"], "INE"));
            
            RuleFor(x => x.ResponsiblePersonPhone)
                .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage(string.Format(localizer["InvalidField"], localizer["ResponsiblePersonPhone"]))
                .When(x => !string.IsNullOrWhiteSpace(x.ResponsiblePersonPhone));

            RuleFor(x => x.ResponsiblePersonName)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["ResponsiblePersonName"]));

        }
    }
}
