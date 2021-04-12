using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Company Responsible Request Validator
    /// </summary>
    public class CompanyResponsibleRequestValidator : AbstractValidator<CompanyResponsibleCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">String localizer</param>
        public CompanyResponsibleRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Name"]));

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => x.Email != null)
                .WithMessage(string.Format(localizer["InvalidField"], localizer["ResponsiblePersonEmail"]));

            RuleFor(x => x.MobilePhone)
                .Cascade(CascadeMode.Stop)
                .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage(string.Format(localizer["InvalidField"], localizer["ResponsiblePersonMobilePhone"]))
                .Must(y =>
                {
                    return !y.StartsWith("55") || (y.Substring(4, 1).Equals("9") && y.Length == 13);
                })
                .WithMessage(string.Format(localizer["InvalidField"], localizer["ResponsiblePersonMobilePhone"]))
                .When(x => !string.IsNullOrWhiteSpace(x.MobilePhone));

            RuleFor(x => x.Landline)
                .Cascade(CascadeMode.Stop)
                .Must(y => Regex.IsMatch(y, "^[0-9]+$")).WithMessage(string.Format(localizer["InvalidField"], localizer["ResponsiblePersonLandline"]))
                .Must(y => { 
                    return !y.StartsWith("55") || Regex.IsMatch(y, "^[0-9].{7,}$"); 
                })
                .WithMessage(string.Format(localizer["InvalidField"], localizer["ResponsiblePersonLandline"]))
                .When(x => !string.IsNullOrWhiteSpace(x.Landline));
        }
    }
}
