using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Users
{
    /// <summary>
    /// User Agent Edit Request Validator
    /// </summary>
    public class UserAgentEditRequestValidator : AbstractValidator<UserAgentEditRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">localizer</param>
        public UserAgentEditRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["IsActive"]));

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["UserId"]));

            RuleFor(x => x.CompleteName)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["CompleteName"]));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Email"]));

            RuleFor(x => x.Email)
               .EmailAddress()
               .When(x => x != null)
               .WithMessage(string.Format(localizer["InvalidField"], localizer["Email"]));

            RuleFor(x => x.Cpf).Cascade(CascadeMode.Stop)
                 .NotEmpty().WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Length(11).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Must(x => CpfUtils.Valid(x)).WithMessage(string.Format(localizer["InvalidField"], "CPF"));

            RuleFor(x => x.CellphoneNumber)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["CellphoneNumber"]))
                .When(x => string.IsNullOrWhiteSpace(x.CorporateCellphoneNumber));

            RuleFor(x => x.CellphoneNumber)
               .Must(y => PhoneNumberUtils.ValidMobile(y))
               .When(y => !string.IsNullOrWhiteSpace(y.CellphoneNumber))
               .WithMessage(string.Format(localizer["InvalidField"], localizer["CellphoneNumber"]));

            RuleFor(x => x.CorporateCellphoneNumber)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["CorporateCellphoneNumber"]))
                .When(x => string.IsNullOrWhiteSpace(x.CellphoneNumber));

            RuleFor(x => x.CorporateCellphoneNumber)
               .Must(y => PhoneNumberUtils.ValidMobile(y))
               .When(y => !string.IsNullOrWhiteSpace(y.CorporateCellphoneNumber))
               .WithMessage(string.Format(localizer["InvalidField"], localizer["CorporateCellphoneNumber"]));

            RuleFor(x => x.Password)
                .Must(y => {
                    return Regex.IsMatch(y, "^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\\d]){1,})(?=(.*[\\W]){1,})(?!.*\\s).{8,}$");
                })
                .WithMessage(localizer["PasswordOutPattern"])
                .When(y => !string.IsNullOrWhiteSpace(y.Password));

            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Company"]));

            RuleFor(x => x.Address)
                .SetValidator(new AddressEditValidator(localizer));
        }
    }
}
