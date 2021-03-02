using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Users
{
    public class GetRegisteredUsersCountRequestValidator : AbstractValidator<GetRegisteredUsersCountRequest>
    {
        public GetRegisteredUsersCountRequestValidator()
        {
           RuleFor(x => x.ProfileType)
                .SetValidator(new RequiredFieldValidator<int>("ProfileType"));
        }
    }
}
