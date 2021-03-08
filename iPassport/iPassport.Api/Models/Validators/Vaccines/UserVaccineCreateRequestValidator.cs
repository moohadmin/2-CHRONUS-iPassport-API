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
                .SetValidator(new RequiredFieldValidator<int>("PriorityGroup", localizer));

            RuleFor(x => x.VaccinationDate)
                .SetValidator(new RequiredFieldValidator<DateTime>("VaccinationDate", localizer));

            RuleFor(x => x.Vaccine)
                .SetValidator(new GuidValidator("Vaccine", localizer));

            RuleFor(x => x.Batch)
                .SetValidator(new RequiredFieldValidator<string>("Batch", localizer));

            RuleFor(x => x.UnitName)
                .SetValidator(new RequiredFieldValidator<string>("UnitName", localizer));
        }
    }
}
