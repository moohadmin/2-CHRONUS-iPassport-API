using FluentValidation;
using iPassport.Api.Models.Requests;
using System;

namespace iPassport.Api.Models.Validators.Plans
{
    public class PassportUseCreateRequestValidator : AbstractValidator<PassportUseCreateRequest>
    {
        public PassportUseCreateRequestValidator()
        {
            RuleFor(s => s.Latitude)
                .SetValidator(new RequiredFieldValidator<string>("Latitude"));

            RuleFor(s => s.Longitude)
                .SetValidator(new RequiredFieldValidator<string>("Longitude"));

            RuleFor(s => s.PassportDetailsId)
                .SetValidator(new GuidValidator("PassportDetailsId"));
        }
    }
}
