using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    /// <summary>
    /// State Create Request Validator
    /// </summary>
    public class StateCreateRequestValidator : AbstractValidator<StateCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">string localizer</param>
        public StateCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Name)
                .SetValidator(new RequiredFieldValidator<string>("Name", localizer));

            RuleFor(s => s.Acronym)
                .SetValidator(new RequiredFieldValidator<string>("Acronym", localizer));

            RuleFor(s => s.IbgeCode)
                .SetValidator(new RequiredFieldValidator<int>("IbgeCode", localizer))
                .GreaterThan(0).WithMessage(string.Format(localizer["InvalidField"], "IbgeCode"));

            RuleFor(s => s.CountryId)
                .SetValidator(new GuidValidator("CountryId", localizer));

            RuleFor(s => s.Population)
                .Must(x =>
                {
                    return x == null || x > 0;
                }).WithMessage(string.Format(localizer["InvalidField"], "Population"));

        }
    }
}
