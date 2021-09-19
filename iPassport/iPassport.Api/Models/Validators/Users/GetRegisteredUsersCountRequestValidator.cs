using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Users
{
    /// <summary>
    /// Get Registered Users Count Request Validator
    /// </summary>
    public class GetRegisteredUsersCountRequestValidator : AbstractValidator<GetRegisteredUsersCountRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">localizer</param>
        public GetRegisteredUsersCountRequestValidator(IStringLocalizer<Resource> localizer)
        {
           RuleFor(x => x.ProfileType)
                .SetValidator(new RequiredFieldValidator<int>("ProfileType", localizer));
        }
    }
}
