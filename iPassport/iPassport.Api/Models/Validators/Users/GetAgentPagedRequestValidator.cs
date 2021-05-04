using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Users
{
    /// <summary>
    /// Get Citzen Paged Request Validator Class
    /// </summary>
    public class GetAgentPagedRequestValidator : AbstractValidator<GetAgentPagedRequest>
    {
        /// <summary>
        /// Get Citzen Paged Request Validator constructor
        /// </summary>
        /// <param name="localizer">resource</param>
        public GetAgentPagedRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Initials)
                .Must(x => string.IsNullOrWhiteSpace(x) || x.Length >= 3).WithMessage(string.Format(localizer["InitalsRequestMin"], "3"))
                .SetValidator(new RequiredFieldValidator<string>("Initials", localizer));

            RuleFor(x => x.Login)
                .Must(x => string.IsNullOrWhiteSpace(x) || x.Length >= 3).WithMessage(string.Format(localizer["InitalsRequestMin"], "3"))
                .SetValidator(new RequiredFieldValidator<string>("Login", localizer));

            When(y => !string.IsNullOrWhiteSpace(y.Cpf), () =>
            {
                RuleFor(x => x.Cpf)
                    .Length(11).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                    .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                    .Must(x => CpfUtils.Valid(x))
                        .WithMessage(string.Format(localizer["InvalidField"], "CPF"));
            });

            RuleFor(x => x.PageNumber)
                .SetValidator(new RequiredFieldValidator<int>(localizer["PageNumber"], localizer));

            RuleFor(x => x.PageSize)
                .SetValidator(new RequiredFieldValidator<int>(localizer["PageSize"], localizer));

        }
    }
}
