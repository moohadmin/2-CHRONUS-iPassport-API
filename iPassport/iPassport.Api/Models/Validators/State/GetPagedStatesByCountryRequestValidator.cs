using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    public class GetPagedStatesByCountryRequestValidator : AbstractValidator<GetPagedStatesByCountryRequest>
    {
        public GetPagedStatesByCountryRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.CountryId)
                .SetValidator(new GuidValidator("CountryId", localizer));

        }
    }
}
