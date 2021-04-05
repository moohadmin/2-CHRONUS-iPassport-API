using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Api.Models.Validators.Vaccines;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Users
{
    /// <summary>
    /// AdminCreateRequestValidator Class
    /// </summary>
    public class AdminCreateRequestValidator : AbstractValidator<AdminCreateRequest>
    {
        /// <summary>
        /// AdminCreateRequestValidator Constructor
        /// </summary>
        /// <param name="localizer">Resource</param>
        public AdminCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
             RuleFor(x => x.CompleteName)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["CompleteName"]));

            RuleFor(x => x.Cpf)            
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], "CPF"));

            RuleFor(x => x.Cpf).Cascade(CascadeMode.Stop)
                .Must(x => Regex.IsMatch(x, "^[0-9]{11}$")).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                .Must(x => CpfUtils.Valid(x)).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                .When(y => !string.IsNullOrWhiteSpace(y.Cpf));

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Email"]));

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(string.Format(localizer["InvalidField"], localizer["Email"]))
                .When(y => !string.IsNullOrWhiteSpace(y.Email));                

            RuleFor(x => x.Telephone)                
                .Must(y =>
                {
                    return !y.StartsWith("55") 
                        || (Regex.IsMatch(y, "^[0-9]{13}$") && y.Substring(4, 1).Equals("9"));
                })
                .WithMessage(string.Format(localizer["InvalidField"], localizer["Telephone"]))
                .When(y => !string.IsNullOrWhiteSpace(y.Telephone));

            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Company"]));
                        
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Password"]));

            RuleFor(x => x.Password)
                .Must(y => {
                    return Regex.IsMatch(y, "^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\\d]){1,})(?=(.*[\\W]){1,})(?!.*\\s).{8,}$");
                })
                .WithMessage(localizer["PasswordOutPattern"])
                .When(y => !string.IsNullOrWhiteSpace(y.Password));

            RuleFor(x => x.ProfileId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Profile"]));

            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["IsActive"]));
        }
    }
}
