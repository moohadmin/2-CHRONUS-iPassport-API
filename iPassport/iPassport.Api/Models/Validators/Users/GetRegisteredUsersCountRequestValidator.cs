using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Users
{
    public class GetRegisteredUsersCountRequestValidator : AbstractValidator<GetRegisteredUsersCountRequest>
    {
        public GetRegisteredUsersCountRequestValidator(IStringLocalizer<Resource> localizer)
        {
           RuleFor(x => x.ProfileType)
                .SetValidator(new RequiredFieldValidator<int>("ProfileType", localizer));
        }
    }
}
