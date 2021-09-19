using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Resend Pin Request Validator
    /// </summary>
    public class ResendPinRequestValidator : AbstractValidator<ResendPinRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public ResendPinRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Phone)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
                   .WithMessage(string.Format(localizer["RequiredField"], localizer["Telephone"]))
               .Must(y => PhoneNumberUtils.ValidMobile(y))
                    .WithMessage(string.Format(localizer["InvalidField"], localizer["Telephone"]));

            RuleFor(s => s.UserId)
                .SetValidator(new GuidValidator("UserId", localizer));
        }
    }
}
