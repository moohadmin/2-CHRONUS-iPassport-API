using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    public class GetPagedCitiesByStateAndNamePartsRequestValidator : AbstractValidator<GetPagedCitiesByStateAndNamePartsRequest>
    {
        public GetPagedCitiesByStateAndNamePartsRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.StateId)
                .SetValidator(new GuidValidator("StateId", localizer));
        }
    }
}
