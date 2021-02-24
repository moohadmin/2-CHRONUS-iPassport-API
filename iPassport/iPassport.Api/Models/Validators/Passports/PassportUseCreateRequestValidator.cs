using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Plans
{
    public class PassportUseCreateRequestValidator : AbstractValidator<PassportUseCreateRequest>
    {
        public PassportUseCreateRequestValidator()
        {
            RuleFor(s => s.Latitude)
                .SetValidator(new LatitudeValidator("Latitude"));

            RuleFor(s => s.Longitude)
                .SetValidator(new LongitudeValidator("Longitude"));

            RuleFor(s => s.PassportDetailsId)
                .SetValidator(new GuidValidator("PassportDetailsId"));
        }
    }
}
