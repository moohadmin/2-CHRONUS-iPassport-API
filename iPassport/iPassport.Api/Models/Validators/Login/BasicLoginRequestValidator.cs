using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Basic Login Request Validator
    /// </summary>
    public class BasicLoginRequestValidator : AbstractValidator<BasicLoginRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public BasicLoginRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Username)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Username"]));
                

            RuleFor(s => s.Password)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Password"]));
        }
    }
}
