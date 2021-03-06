using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    public class CountryCreateRequestValidator : AbstractValidator<CountryCreateRequest>
    {
        public CountryCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Name)
                .SetValidator(new RequiredFieldValidator<string>("Name", localizer));

            RuleFor(s => s.Acronym)
                .SetValidator(new RequiredFieldValidator<string>("Acronym", localizer));

            RuleFor(s => s.ExternalCode)
                .SetValidator(new RequiredFieldValidator<string>("ExternalCode", localizer));

            RuleFor(s => s.Population)
                .Must(x =>
                {
                    return x == null || x > 0;
                }).WithMessage(string.Format(localizer["InvalidField"], "Population"));

        }
    }
}
