using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Users
{
    /// <summary>
    /// Get Admin User Paged Request Validator
    /// </summary>
    public class GetAdminUserPagedRequestValidator:  AbstractValidator<GetAdminUserPagedRequest>
    {
        /// <summary>
        /// Get Admin User Paged Request Validator Constructor
        /// </summary>
        /// <param name="localizer">resource</param>
        public GetAdminUserPagedRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Initials)
                .Must(x => string.IsNullOrWhiteSpace(x) || x.Length >= 3).WithMessage(string.Format(localizer["InitalsRequestMin"], "3"))
                .SetValidator(new RequiredFieldValidator<string>("Initials", localizer));

            RuleFor(x => x.PageNumber)
                .SetValidator(new RequiredFieldValidator<int>(localizer["PageNumber"], localizer));

            RuleFor(x => x.PageSize)
                .SetValidator(new RequiredFieldValidator<int>(localizer["PageSize"], localizer));
        }
    }
}
