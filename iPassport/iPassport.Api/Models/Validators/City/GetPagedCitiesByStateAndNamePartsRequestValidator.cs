using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Get Paged Cities By State And Name Parts Request Validator
    /// </summary>
    public class GetPagedCitiesByStateAndNamePartsRequestValidator : AbstractValidator<GetPagedCitiesByStateAndNamePartsRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">String localizer</param>
        public GetPagedCitiesByStateAndNamePartsRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.StateId)
                .SetValidator(new GuidValidator("StateId", localizer));
        }
    }
}
