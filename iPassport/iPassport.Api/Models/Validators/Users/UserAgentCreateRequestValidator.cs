using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Users
{
    /// <summary>
    /// User Agent Create Request Validator
    /// </summary>
    public class UserAgentCreateRequestValidator : AbstractValidator<UserAgentCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">localizer</param>
        public UserAgentCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.FullName)
                .SetValidator(new RequiredFieldValidator<string>("FullName", localizer));

            RuleFor(x => x.CPF).Cascade(CascadeMode.Stop)
                 .NotEmpty().WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Length(11).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Must(x => CpfUtils.Valid(x)).WithMessage(string.Format(localizer["InvalidField"], "CPF"));

            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
               .NotEmpty()
                   .WithMessage(string.Format(localizer["RequiredField"], "Mobile"))
               .Must(y => PhoneNumberUtils.ValidMobile(y))
                    .WithMessage(string.Format(localizer["InvalidField"], "Mobile"));

            RuleFor(x => x.Username)
                .SetValidator(new RequiredFieldValidator<string>("Username", localizer));

            RuleFor(x => x.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password", localizer));

            RuleFor(x => x.PasswordIsValid)
                .SetValidator(new RequiredFieldValidator<bool>("PasswordIsValid", localizer));

            RuleFor(x => x.CompanyId)
                .SetValidator(new GuidValidator("CompanyId", localizer));
        }
    }
}
