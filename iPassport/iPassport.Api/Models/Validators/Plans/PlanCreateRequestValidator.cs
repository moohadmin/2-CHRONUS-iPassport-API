using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Plans
{
    public class PlanCreateRequestValidator : AbstractValidator<PlanCreateRequest>
    {
        public PlanCreateRequestValidator()
        {
            RuleFor(s => s.Type)
                .SetValidator(new RequiredFieldValidator<string>("Type"));

            RuleFor(s => s.Description)
                .SetValidator(new RequiredFieldValidator<string>("Description"));
        }
    }
}
