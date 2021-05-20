using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.HealthUnit
{
    /// <summary>
    /// Health Unit Responsible Request Validator
    /// </summary>
    public class HealthUnitResponsibleEditRequestValidator : AbstractValidator<HealthUnitResponsibleEditRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">String localizer</param>
        public HealthUnitResponsibleEditRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], "Id"));

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["ResponsiblePersonName"]));

            RuleFor(x => x.MobilePhone)
                .Must(y => PhoneNumberUtils.ValidMobile(y))
                .WithMessage(string.Format(localizer["InvalidField"], localizer["ResponsiblePersonMobilePhone"]))
                .When(x => !string.IsNullOrWhiteSpace(x.MobilePhone));

            RuleFor(x => x.Landline)
                .Must(y => PhoneNumberUtils.ValidLandline(y))
                .WithMessage(string.Format(localizer["InvalidField"], localizer["ResponsiblePersonLandline"]))
                .When(x => !string.IsNullOrWhiteSpace(x.Landline));
        }
    }
}
