using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Vaccines
{
    /// <summary>
    /// UserVaccineEditRequestValidator Class
    /// </summary>
    public class UserVaccineEditRequestValidator : AbstractValidator<UserVaccineEditRequest>
    {
        /// <summary>
        /// UserVaccineEditRequestValidator Contructor
        /// </summary>
        /// <param name="localizer">Resource</param>
        public UserVaccineEditRequestValidator(IStringLocalizer<Resource> localizer)
        {
            
            RuleFor(x => x.Dose)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "Dose"));

            RuleFor(x => x.VaccinationDate)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "VaccinationDate"))
                .LessThanOrEqualTo(DateTime.UtcNow).When(x => x.VaccinationDate.HasValue).WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.Vaccine)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "Vaccine"));

            RuleFor(x => x.Batch)
                .SetValidator(new RequiredFieldValidator<string>("Batch", localizer));

            RuleFor(x => x.HealthUnitId)
                .NotEmpty().WithMessage(string.Format(localizer["RequiredField"], "HealthUnitId"));

            RuleFor(x => x.EmployeeCpf)
                .Cascade(CascadeMode.Stop)
                .SetValidator(new RequiredFieldValidator<string>("EmployeeCpf", localizer)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpf))
                .Length(11).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpf)).WithMessage(string.Format(localizer["InvalidField"], "EmployeeCpf"))
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpf)).WithMessage(string.Format(localizer["InvalidField"], "EmployeeCpf"))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpf)).WithMessage(string.Format(localizer["InvalidField"], "EmployeeCpf"));
        }
    }
}
