using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Users
{
    public class UserAgentCreateRequestValidator : AbstractValidator<UserAgentCreateRequest>
    {
        public UserAgentCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Address)
                .SetValidator(new RequiredFieldValidator<AddressCreateRequest>("Address", localizer))
                .SetValidator(new AddressValidator(localizer, true));

            RuleFor(x => x.CPF).Cascade(CascadeMode.Stop)
                 .Length(11).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Must(x => CpfUtils.Valid(x)).WithMessage(string.Format(localizer["InvalidField"], "CPF"));

            RuleFor(x => x.FullName)
                .SetValidator(new RequiredFieldValidator<string>("FullName", localizer));

            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)                
                .Must(y => { return string.IsNullOrWhiteSpace(y) || y.Substring(4, 1).Equals("9"); })
                    .WithMessage(string.Format(localizer["InvalidField"], "Phone"));

            RuleFor(x => x.Mobile).Must(y => {return string.IsNullOrWhiteSpace(y) || Regex.IsMatch(y, "^[0-9]{13}$"); })
                .WithMessage(string.Format(localizer["InvalidField"], "Phone"));

            RuleFor(x => x.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password", localizer));

            RuleFor(x => x.PasswordIsValid)
                .SetValidator(new RequiredFieldValidator<bool>("PasswordIsValid", localizer));

            RuleFor(x => x.Username)
                .SetValidator(new RequiredFieldValidator<string>("Username", localizer));

            RuleFor(x => x.CompanyId)
                .SetValidator(new GuidValidator("CompanyId", localizer));
        }
    }
}
