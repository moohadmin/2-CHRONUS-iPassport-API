using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    public class PlanCreateRequestValidator : AbstractValidator<PlanCreateRequest>
    {
        public PlanCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Type)
                .SetValidator(new RequiredFieldValidator<string>("Type", localizer));

            RuleFor(s => s.Description)
                .SetValidator(new RequiredFieldValidator<string>("Description", localizer));
        }
    }
}
