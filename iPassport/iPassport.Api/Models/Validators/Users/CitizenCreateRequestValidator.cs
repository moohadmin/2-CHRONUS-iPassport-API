using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Api.Models.Validators.Vaccines;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;
using System;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Users
{
    /// <summary>
    /// Citizen Create Request Validator
    /// </summary>
    public class CitizenCreateRequestValidator : AbstractValidator<CitizenCreateRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">localizer</param>
        public CitizenCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.CompleteName)
                .SetValidator(new RequiredFieldValidator<string>(localizer["CompleteName"], localizer));

            RuleFor(s => s.Address)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Address"]));

            RuleFor(x => x.Address)
                .SetValidator(new AddressValidator(localizer));

            RuleFor(x => x.Birthday)
                .Cascade(CascadeMode.Stop)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], localizer["Birthday"]))
                .SetValidator(new RequiredFieldValidator<DateTime?>(localizer["Birthday"], localizer))
                .Must(x => x.Value.Date <= DateTime.UtcNow.Date).WithMessage(localizer["BirthdayCannotBeHiggerThenActualDate"])
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-200)).WithMessage(string.Format(localizer["InvalidField"], localizer["Birthday"]));

            RuleFor(x => x.Cpf)
                .Cascade(CascadeMode.Stop)
                .SetValidator(new RequiredFieldValidator<string>("CPF", localizer)).When(x => string.IsNullOrWhiteSpace(x.Cns))
                .Length(11).When(x => !string.IsNullOrWhiteSpace(x.Cpf)).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                .Must(x => Regex.IsMatch(x, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.Cpf)).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                .Must(x => CpfUtils.Valid(x)).When(x => !string.IsNullOrWhiteSpace(x.Cpf)).WithMessage(string.Format(localizer["InvalidField"], "CPF"));

            RuleFor(x => x.Cns)
                .Cascade(CascadeMode.Stop)
                .SetValidator(new RequiredFieldValidator<string>("Cns", localizer)).When(x => string.IsNullOrWhiteSpace(x.Cpf))
                .Length(15).When(x => !string.IsNullOrWhiteSpace(x.Cns)).WithMessage(string.Format(localizer["InvalidField"], "CNS"))
                .Must(y => Regex.IsMatch(y, "^[0-9]+$")).When(x => !string.IsNullOrWhiteSpace(x.Cns)).WithMessage(string.Format(localizer["InvalidField"], "CNS"));

            RuleFor(x => new { x.Cns, x.Cpf })
                .Must(x => !string.IsNullOrWhiteSpace(x.Cpf) || !string.IsNullOrWhiteSpace(x.Cns))
                .WithMessage(localizer["CnsAndCpfRequired"]);

            RuleFor(x => x.Telephone)
                .Must(y => PhoneNumberUtils.ValidMobile(y))
                .When(x => !string.IsNullOrWhiteSpace(x.Telephone))
                .WithMessage(string.Format(localizer["InvalidField"], localizer["Telephone"]));

            RuleFor(x => x.NumberOfDoses)
                .Must(x => x >= 0).WithMessage(string.Format(localizer["RequiredField"], localizer["NumberOfDoses"]));

            RuleForEach(x => x.Doses)
                .NotNull().When(x => x.NumberOfDoses > 0).WithMessage(string.Format(localizer["RequiredField"], "Doses"));
            
            RuleForEach(x => x.Doses)
                .SetValidator(new UserVaccineCreateRequestValidator(localizer)).When(x => x.NumberOfDoses > 0);

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => x != null)
                .WithMessage(string.Format(localizer["InvalidField"], "E-mail"));

            RuleFor(x => x.Test)
                .Null()
                    .When(x => !x.WasTestPerformed.GetValueOrDefault())
                    .WithMessage(localizer["TestMustBeNull"]);

            RuleFor(y => y.Test)
                 .NotNull()
                     .When(x => x.WasTestPerformed.GetValueOrDefault())
                     .WithMessage(localizer["TestNotMustBeNullOrEmpty"])
                 .SetValidator(new UserDiseaseTestCreateRequestValidator(localizer))
                     .When(x => x.WasTestPerformed.GetValueOrDefault());
        }
    }
}
