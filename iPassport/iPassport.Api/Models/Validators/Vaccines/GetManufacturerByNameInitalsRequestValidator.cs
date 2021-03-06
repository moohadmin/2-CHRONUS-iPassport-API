using FluentValidation;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Vaccines
{
    public class GetManufacturerByNameInitalsRequestValidator : AbstractValidator<GetByNamePartsPagedRequest>
    {
        public GetManufacturerByNameInitalsRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Initials)
                .Must(x => string.IsNullOrWhiteSpace(x) || x.Length >= 3).WithMessage(string.Format(localizer["InitalsRequestMin"], "3"))
                .SetValidator(new RequiredFieldValidator<string>("Initials", localizer));

            RuleFor(x => x.PageNumber)
                .SetValidator(new RequiredFieldValidator<int>("PageNumber", localizer));

            RuleFor(x => x.PageSize)
                .SetValidator(new RequiredFieldValidator<int>("PageSize", localizer));
        }
    }
}
