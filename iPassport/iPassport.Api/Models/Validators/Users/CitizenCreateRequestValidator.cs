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
    public class CitizenCreateRequestValidator : AbstractValidator<CitizenCreateRequest>
    {
        public CitizenCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.CompleteName)
                .SetValidator(new RequiredFieldValidator<string>("CompleteName", localizer));

            RuleFor(x => x.Address)
                .SetValidator(new AddressValidator(localizer, false));

            RuleFor(x => x.Birthday)
                .Cascade(CascadeMode.Stop)
                .Must(x => x.HasValue).WithMessage(string.Format(localizer["RequiredField"], "Birthday"))
                .SetValidator(new RequiredFieldValidator<DateTime?>("Birthday", localizer))
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(string.Format(localizer["InvalidField"], "Birthday"))
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-200)).WithMessage(string.Format(localizer["InvalidField"], "Birthday"));

            RuleFor(x => x.Cpf)
                .Cascade(CascadeMode.Stop)
                .SetValidator(new RequiredFieldValidator<string>("Cpf", localizer)).When(x => string.IsNullOrWhiteSpace(x.Cns))
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

            RuleFor(x => x.PriorityGroupId)
                .SetValidator(new GuidValidator("PriorityGroupId", localizer));

            RuleFor(x => x.Telephone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(string.Format(localizer["InvalidField"], "Telephone"))
                .Must(y =>
                {
                    return !y.StartsWith("55") || (y.Substring(4, 1).Equals("9") && y.Length == 13);
                })
                .WithMessage(string.Format(localizer["InvalidField"], "Telephone"))
                .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage(string.Format(localizer["InvalidField"], "Telephone"));

            RuleFor(x => x.NumberOfDoses)
                .Must(x => x >= 0).WithMessage(string.Format(localizer["RequiredField"], "NumberOfDoses"));

            RuleForEach(x => x.Doses)
                .Cascade(CascadeMode.Stop)
                .NotNull().When(x => x.NumberOfDoses > 0).WithMessage(string.Format(localizer["RequiredField"], "Doses"))
                .SetValidator(new UserVaccineCreateRequestValidator(localizer)).When(x => x.NumberOfDoses > 0);

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => x != null)
                .WithMessage(string.Format(localizer["InvalidField"], "Email"));

            RuleFor(x => x.Test)
                .SetValidator(new UserDiseaseTestCreateRequestValidator(localizer))
                .When(x => x.Test != null);

            
        }
    }
}
