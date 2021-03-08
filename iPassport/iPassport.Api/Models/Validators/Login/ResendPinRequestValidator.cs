using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Plans
{
    public class ResendPinRequestValidator : AbstractValidator<ResendPinRequest>
    {
        public ResendPinRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Phone)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
                   .WithMessage(string.Format(localizer["InvalidField"], "Phone"))
               .Must(y => {
                   return !y.StartsWith("55") || (y.Substring(4, 1).Equals("9") && y.Length == 13);
               }).WithMessage(string.Format(localizer["InvalidField"], "Phone"));

            RuleFor(x => x.Phone).Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage(string.Format(localizer["InvalidField"], "Phone"));

            RuleFor(s => s.UserId)
                .SetValidator(new GuidValidator("UserId", localizer));
        }
    }
}
