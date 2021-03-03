using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Vaccines
{
    public class GetManufacturerByNameInitalsRequestValidator : AbstractValidator<GetByNameInitalsPagedRequest>
    {
        public GetManufacturerByNameInitalsRequestValidator()
        {
            RuleFor(x => x.Initials)
                .MinimumLength(3).WithMessage("Necessário o minimo 3 de caracteres para realizar a pesquisa;")
                .SetValidator(new RequiredFieldValidator<string>("Initials"));

            RuleFor(x => x.PageNumber)
                .SetValidator(new RequiredFieldValidator<int>("PageNumber"));

            RuleFor(x => x.PageSize)
                .SetValidator(new RequiredFieldValidator<int>("PageSize"));
        }
    }
}
