using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// City Create Request Validator
    /// </summary>
    public class CityCreateRequestValidator : AbstractValidator<CityCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">String localizer</param>
        public CityCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Name)
                .SetValidator(new RequiredFieldValidator<string>("Name", localizer));

            RuleFor(s => s.IbgeCode)
                .SetValidator(new RequiredFieldValidator<int>("IbgeCode", localizer))
                .GreaterThan(0).WithMessage(string.Format(localizer["InvalidField"], "IbgeCode"));

            RuleFor(s => s.StateId)
                .SetValidator(new GuidValidator("StateId", localizer));

            RuleFor(s => s.Population)
                .Must(x =>
                {
                    return x == null || x > 0;
                }).WithMessage(string.Format(localizer["InvalidField"], "Population"));

        }
    }
}
