using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    public class EmailLoginRequestValidator : AbstractValidator<EmailLoginRequest>
    {
        public EmailLoginRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Email)
                .SetValidator(new RequiredFieldValidator<string>("E-mail", localizer))
                .EmailAddress().WithMessage(string.Format(localizer["InvalidField"], "E-mail"));               

            RuleFor(s => s.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password", localizer));
        }
    }
}
