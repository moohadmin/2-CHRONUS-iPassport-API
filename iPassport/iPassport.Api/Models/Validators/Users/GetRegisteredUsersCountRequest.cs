using FluentValidation;
using iPassport.Api.Models.Requests;
using Microsoft.AspNetCore.Http;

namespace iPassport.Api.Models.Validators.Users
{
    public class GetRegisteredUsersCountRequestValidator : AbstractValidator<GetRegisteredUsersCountRequest>
    {
        public GetRegisteredUsersCountRequestValidator()
        {
           RuleFor(x => x.ProfileType)
                .IsInEnum().WithMessage("O campo ProfileType está inválido");
        }
    }
}
