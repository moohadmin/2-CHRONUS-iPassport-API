using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    public class BasicLoginRequestValidator : AbstractValidator<BasicLoginRequest>
    {
        public BasicLoginRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Username)
                .SetValidator(new RequiredFieldValidator<string>("Username", localizer));

            RuleFor(s => s.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password", localizer));
        }
    }
}
