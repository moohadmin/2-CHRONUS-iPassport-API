using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System;

namespace iPassport.Api.Models.Validators.Vaccines
{
    public class UserVaccineCreateRequestValidator : AbstractValidator<UserVaccineCreateRequest>
    {
        public UserVaccineCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Dose)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "Dose"));

            RuleFor(x => x.VaccinationDate)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "VaccinationDate"));

            RuleFor(x => x.Vaccine)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "Vaccine"));

            RuleFor(x => x.Batch)
                .SetValidator(new RequiredFieldValidator<string>("Batch", localizer));

            RuleFor(x => x.UnitName)
                .SetValidator(new RequiredFieldValidator<string>("UnitName", localizer));
        }
    }
}
