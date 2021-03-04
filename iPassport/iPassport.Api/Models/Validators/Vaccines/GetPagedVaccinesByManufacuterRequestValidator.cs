using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Vaccines
{
    public class GetPagedVaccinesByManufacuterRequestValidator : AbstractValidator<GetPagedVaccinesByManufacuterRequest>
    {
        public GetPagedVaccinesByManufacuterRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.ManufacuterId)
                .SetValidator(new GuidValidator("ManufacuterId", localizer));

            RuleFor(x => x.PageNumber)
                .SetValidator(new RequiredFieldValidator<int>("PageNumber", localizer));

            RuleFor(x => x.PageSize)
                .SetValidator(new RequiredFieldValidator<int>("PageSize", localizer));
        }
    }
}
