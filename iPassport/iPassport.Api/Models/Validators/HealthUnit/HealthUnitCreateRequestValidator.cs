using FluentValidation;
using iPassport.Api.Models.Requests.HealthUnit;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.HealthUnit
{
    /// <summary>
    /// Health Unit Create Request Validator
    /// </summary>
    public class HealthUnitCreateRequestValidator : AbstractValidator<HealthUnitCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">localizer</param>
        public HealthUnitCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Name)
                .SetValidator(new RequiredFieldValidator<string>("Name", localizer));

            //RuleFor(x => x.Cnpj)
            //    .Cascade(CascadeMode.Stop)
            //    .SetValidator(new RequiredFieldValidator<string>("Cnpj", localizer)).When(x => string.IsNullOrWhiteSpace(x.Ine))
            //    .Must(x => CnpjUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.Cnpj)).WithMessage(string.Format(localizer["InvalidField"], "CNPJ"));

            //RuleFor(x => x.Ine)
            //    .Cascade(CascadeMode.Stop)
            //    .SetValidator(new RequiredFieldValidator<string>("Ine", localizer)).When(x => string.IsNullOrWhiteSpace(x.Cnpj))
            //    .MaximumLength(10).When(x => !string.IsNullOrWhiteSpace(x.Ine)).WithMessage(string.Format(localizer["InvalidField"], "INE"))
            //    .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.Ine)).WithMessage(string.Format(localizer["InvalidField"], "INE"));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(string.Format(localizer["InvalidField"], "E-mail")).When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.IsActive)
                .SetValidator(new RequiredFieldValidator<bool?>("IsActive", localizer));

            RuleFor(x => x.TypeId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], "TypeId"));

            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], "CompanyId"));

            RuleFor(x => x.Address)
                .SetValidator(new AddressValidator(localizer, false));
        }
    }
}
