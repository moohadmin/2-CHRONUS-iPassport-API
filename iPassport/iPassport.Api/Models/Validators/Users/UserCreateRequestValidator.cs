using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Users
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Address)
                .SetValidator(new RequiredFieldValidator<string>("Address", localizer));

            RuleFor(x => x.Birthday)
                .SetValidator(new RequiredFieldValidator<DateTime>("Birthday", localizer))
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(string.Format(localizer["InvalidField"], "Birthday"))
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-200)).WithMessage(string.Format(localizer["InvalidField"], "Birthday"));

            RuleFor(x => x.BloodType)
                .SetValidator(new RequiredFieldValidator<string>("BloodType", localizer));

            RuleFor(x => x.Breed)
                .SetValidator(new RequiredFieldValidator<string>("Breed", localizer));

            RuleFor(x => x.CNS)
                .Length(15).WithMessage(string.Format(localizer["InvalidField"], "CNS"))
                .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage(string.Format(localizer["InvalidField"], "CNS"));

            RuleFor(x => x.CPF).Cascade(CascadeMode.Stop)
                 .Length(11).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Must(x => CpfUtils.Valid(x)).WithMessage(string.Format(localizer["InvalidField"], "CPF"));

            RuleFor(x => x.Email)
                .SetValidator(new RequiredFieldValidator<string>("E-mail", localizer))
                .EmailAddress().WithMessage(string.Format(localizer["InvalidField"], "E-mail"));

            RuleFor(x => x.FullName)
                .SetValidator(new RequiredFieldValidator<string>("FullName", localizer));

            RuleFor(x => x.Gender)
                .SetValidator(new RequiredFieldValidator<string>("Gender", localizer));

            RuleFor(x => x.InternationalDocument)
                 .Length(1, 15).WithMessage(string.Format(localizer["InvalidField"], "InternationalDocument"))
                 .Must(y => Regex.IsMatch(y, "^[1-9a-zA-Z]+$")).WithMessage(string.Format(localizer["InvalidField"], "InternationalDocument"));

            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(string.Format(localizer["InvalidField"], "Phone"))
                .Length(13)
                    .WithMessage(string.Format(localizer["InvalidField"], "Phone"))
                .Must(y => y.Substring(4, 1).Equals("9"))
                    .WithMessage(string.Format(localizer["InvalidField"], "Phone"));
            RuleFor(x => x.Mobile).Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage(string.Format(localizer["InvalidField"], "Phone"));

            RuleFor(x => x.Occupation)
                .SetValidator(new RequiredFieldValidator<string>("Occupation", localizer));

            RuleFor(x => x.Passport)
                .Length(3, 15).WithMessage(string.Format(localizer["InvalidField"], "Passport"))
                .Must(y => Regex.IsMatch(y, "^[a-zA-Z]{2}[0-9]+$"))
                    .WithMessage(string.Format(localizer["InvalidField"], "Passport"));

            RuleFor(x => x.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password", localizer));

            RuleFor(x => x.PasswordIsValid)
                .SetValidator(new RequiredFieldValidator<bool>("PasswordIsValid", localizer));

            RuleFor(x => x.Profile)
                .GreaterThanOrEqualTo(0).WithMessage(string.Format(localizer["InvalidField"], "Profile"))
                .LessThanOrEqualTo(2).WithMessage(string.Format(localizer["InvalidField"], "Profile"));

            RuleFor(x => x.RG)
                .Length(1, 15).WithMessage(string.Format(localizer["InvalidField"], "RG"))
                .Must(y => Regex.IsMatch(y, "^[0-9a-zA-Z]+$"))
                    .WithMessage(string.Format(localizer["InvalidField"], "RG"));

            RuleFor(x => x.Username)
                .SetValidator(new RequiredFieldValidator<string>("Username", localizer));
        }
    }
}
