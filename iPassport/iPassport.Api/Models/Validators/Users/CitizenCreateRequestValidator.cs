using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Api.Models.Validators.Vaccines;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
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

            RuleFor(x => x.Gender)
                .SetValidator(new RequiredFieldValidator<EGendersTypes>("Gender", localizer));

            RuleFor(x => x.Address)
                .SetValidator(new AddressValidator(localizer, false));

            RuleFor(x => x.Birthday)
                .SetValidator(new RequiredFieldValidator<DateTime>("Birthday", localizer))
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(string.Format(localizer["InvalidField"], "Birthday"))
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-200)).WithMessage(string.Format(localizer["InvalidField"], "Birthday"));
            
            RuleFor(x => x.Cpf).Cascade(CascadeMode.Stop)
                 .Length(11).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage(string.Format(localizer["InvalidField"], "CPF"))
                 .Must(x => CpfUtils.Valid(x)).WithMessage(string.Format(localizer["InvalidField"], "CPF"));

            RuleFor(x => x.Cns)
                .Length(15).WithMessage(string.Format(localizer["InvalidField"], "CNS"))
                .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage(string.Format(localizer["InvalidField"], "CNS"));

            RuleFor(x => x.CompanyId)
                .SetValidator(new GuidValidator("CompanyId", localizer));

            RuleFor(x => x.PriorityGroup)
                .SetValidator(new RequiredFieldValidator<string>("PriorityGroup", localizer));

            RuleFor(x => x.Breed)
                .SetValidator(new RequiredFieldValidator<EBreedTypes>("Breed", localizer));

            RuleFor(x => x.BloodType)
                .SetValidator(new RequiredFieldValidator<string>("BloodType", localizer));

            RuleFor(x => x.Telephone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(string.Format(localizer["InvalidField"], "Phone"))
                .Length(13)
                    .WithMessage(string.Format(localizer["InvalidField"], "Phone"))
                .Must(y => y.Substring(4, 1).Equals("9"))
                    .WithMessage(string.Format(localizer["InvalidField"], "Phone"));
            
            RuleFor(x => x.Telephone).Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage(string.Format(localizer["InvalidField"], "Phone"));

            RuleFor(x => x.NumberOfDoses)
                .Must(x => x >= 0).WithMessage(string.Format(localizer["RequiredField"], "NumberOfDoses"));

            RuleForEach(x => x.Doses)
                .NotNull().When(x => x.NumberOfDoses > 0).WithMessage(string.Format(localizer["RequiredField"], "Doses"))
                .SetValidator(new UserVaccineCreateRequestValidator(localizer)).When(x => x.NumberOfDoses > 0);
        }
    }
}
