using FluentValidation;
using iPassport.Api.Models.Requests;
using System;

namespace iPassport.Api.Models.Validators.Indicators
{
    public class GetVaccinatedCountRequestValidator : AbstractValidator<GetVaccinatedCountRequest>
    {
        public GetVaccinatedCountRequestValidator()
        {
            RuleFor(s => s.StartTime)
                .SetValidator(new RequiredFieldValidator<DateTime>("StartTime"));

            RuleFor(s => s.EndTime)
                .SetValidator(new RequiredFieldValidator<DateTime>("EndTime"));

            RuleFor(s => s.EndTime)
                .SetValidator(new RequiredFieldValidator<DateTime>("EndTime"));

            RuleFor(s => s.DiseaseId)
                .SetValidator(new RequiredFieldValidator<Guid>("DiseaseId"));

            RuleFor(s => s.DosageCount)
                .SetValidator(new RequiredFieldValidator<int>("DosageCount"));
        }
    }
}
