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
