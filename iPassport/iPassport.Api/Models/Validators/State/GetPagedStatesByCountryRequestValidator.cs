using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// Get Paged States By Country Request Validator
    /// </summary>
    public class GetPagedStatesByCountryRequestValidator : AbstractValidator<GetPagedStatesByCountryRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public GetPagedStatesByCountryRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.CountryId)
                .SetValidator(new GuidValidator("CountryId", localizer));

        }
    }
}
