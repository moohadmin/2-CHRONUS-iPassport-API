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
    /// User Vaccine Create Request Validator
    /// </summary>
    public class UserVaccineCreateRequestValidator : AbstractValidator<UserVaccineCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">localizer</param>
        public UserVaccineCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Dose)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "Dose"));

            RuleFor(x => x.VaccinationDate).Cascade(CascadeMode.Stop)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], localizer["VaccinationDate"]))
                .Must(x => x.Value.Date <= DateTime.UtcNow.Date)
                    .WithMessage(localizer["VaccinationDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.Vaccine)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], localizer["Vaccine"]));

            RuleFor(x => x.Batch)
                .SetValidator(new RequiredFieldValidator<string>(localizer["Batch"], localizer));

            RuleFor(x => x.HealthUnitId)
                .SetValidator(new GuidValidator(localizer["HealthUnitId"], localizer));

            RuleFor(x => x.EmployeeCpf)
                .Cascade(CascadeMode.Stop)
                .SetValidator(new RequiredFieldValidator<string>(localizer["EmployeeCpf"], localizer)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpf))
                .Length(11).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpf)).WithMessage(string.Format(localizer["InvalidField"], localizer["EmployeeCpf"]))
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpf)).WithMessage(string.Format(localizer["InvalidField"], localizer["EmployeeCpf"]))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.EmployeeCpf)).WithMessage(string.Format(localizer["InvalidField"], localizer["EmployeeCpf"]));
        }
    }
}
