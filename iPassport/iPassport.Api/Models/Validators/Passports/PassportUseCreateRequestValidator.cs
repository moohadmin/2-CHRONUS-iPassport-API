using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    public class PassportUseCreateRequestValidator : AbstractValidator<PassportUseCreateRequest>
    {
        public PassportUseCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Latitude)
                .SetValidator(new RequiredFieldValidator<double>("Latitude", localizer))
                .LessThanOrEqualTo(90).WithMessage(string.Format(localizer["RangeField"], "Latitude", "-90", "90"))
                .GreaterThanOrEqualTo(-90).WithMessage(string.Format(localizer["RangeField"], "Latitude", "-90", "90"));

            RuleFor(x => x.Longitude)
                .SetValidator(new RequiredFieldValidator<double>("Longitude", localizer))
                .LessThanOrEqualTo(180).WithMessage(string.Format(localizer["RangeField"], "Longitude", "-180", "180"))
                .GreaterThanOrEqualTo(-180).WithMessage(string.Format(localizer["RangeField"], "Longitude", "-180", "180"));

            RuleFor(s => s.PassportDetailsId)
                .SetValidator(new GuidValidator("PassportDetailsId", localizer));
        }
    }
}
