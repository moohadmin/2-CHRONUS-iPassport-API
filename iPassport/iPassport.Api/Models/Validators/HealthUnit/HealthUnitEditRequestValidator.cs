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
        /// <param name="localizer">localizer</param>
        public HealthUnitEditRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], "Id"));

            RuleFor(x => x.Name)
                .SetValidator(new RequiredFieldValidator<string>("Name", localizer));

            RuleFor(x => x.Cnpj)
                .Cascade(CascadeMode.Stop)
                .SetValidator(new RequiredFieldValidator<string>("Cnpj", localizer)).When(x => string.IsNullOrWhiteSpace(x.Ine))
                .Must(x => CnpjUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.Cnpj)).WithMessage(string.Format(localizer["InvalidField"], "Cnpj"));

            RuleFor(x => x.Ine)
                .Cascade(CascadeMode.Stop)
                .SetValidator(new RequiredFieldValidator<string>("Ine", localizer)).When(x => string.IsNullOrWhiteSpace(x.Cnpj))
                .Length(10).When(x => !string.IsNullOrWhiteSpace(x.Ine)).WithMessage(string.Format(localizer["InvalidField"], "INE"))
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.Ine)).WithMessage(string.Format(localizer["InvalidField"], "INE"));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(string.Format(localizer["InvalidField"], "E-mail")).When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.IsActive)
                .SetValidator(new RequiredFieldValidator<bool?>("IsActive", localizer));

            RuleFor(x => x.Address)
                .SetValidator(new AddressEditValidator(localizer));

            RuleFor(x => x.TypeId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], "TypeId"));
        }
    }
}
