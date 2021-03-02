using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Domain.Utils;
using System;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Users
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(x => x.Address)
                .SetValidator(new RequiredFieldValidator<string>("Address"));

            RuleFor(x => x.Birthday)
                .SetValidator(new RequiredFieldValidator<DateTime>("Birthday"))
                .LessThanOrEqualTo(DateTime.Now).WithMessage("O campo Birthday não é valido")
                .GreaterThanOrEqualTo(DateTime.Now.AddYears(-200)).WithMessage("O campo Birthday não é valido");

            RuleFor(x => x.BloodType)
                .SetValidator(new RequiredFieldValidator<string>("BloodType"));

            RuleFor(x => x.Breed)
                .SetValidator(new RequiredFieldValidator<string>("Breed"));

            RuleFor(x => x.CNS)
                .Length(15).WithMessage("o campo CNS não é um CNS Valido")
                .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage("o campo CNS não é um CNS Valido");

            RuleFor(x => x.CPF).Cascade(CascadeMode.Stop)
                 .Length(11).WithMessage($"O campo CPF não é um CPF Valido")
                 .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage($"O campo CPF não é um CPF Valido")
                 .Must(x => CpfUtils.Valid(x)).WithMessage($"O campo Document não é um CPF Valido");

            RuleFor(x => x.Email)
                .SetValidator(new RequiredFieldValidator<string>("E-mail"))
                .EmailAddress().WithMessage("E-mail inválido");

            RuleFor(x => x.FullName)
                .SetValidator(new RequiredFieldValidator<string>("FullName"));

            RuleFor(x => x.Gender)
                .SetValidator(new RequiredFieldValidator<string>("Gender"));

            RuleFor(x => x.InternationalDocument)
                 .Length(1, 15).WithMessage("O campo Document não é um Cod. de Identificação Valido")
                 .Must(y => Regex.IsMatch(y, "^[1-9a-zA-Z]+$")).WithMessage("O campo Document não é um Cod. de Identificação Valido");

            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("O número de telefone informado não é válido. Por favor, verifique")
                .Length(13)
                    .WithMessage("O número de telefone informado não é válido. Por favor, verifique")
                .Must(y => y.Substring(4, 1).Equals("9"))
                    .WithMessage("A informação inserida para o nono dígito não é válida.");
            RuleFor(x => x.Mobile).Must(y => Regex.IsMatch(y, "^[0-9]+$"))
                .WithMessage("O número de telefone informado não é válido. Por favor, verifique");

            RuleFor(x => x.Occupation)
                .SetValidator(new RequiredFieldValidator<string>("Occupation"));

            RuleFor(x => x.Passport)
                .Length(3, 15).WithMessage("O campo Document não é um Passaporte Valido")
                .Must(y => Regex.IsMatch(y, "^[a-zA-Z]{2}[0-9]+$"))
                    .WithMessage("O campo Document não é um Passaporte Valido");

            RuleFor(x => x.Password)
                .SetValidator(new RequiredFieldValidator<string>("Password"));

            RuleFor(x => x.PasswordIsValid).NotNull()
                .WithMessage("O campo PasswordIsValid não pode ser nulo");

            RuleFor(x => x.Profile)
                .GreaterThanOrEqualTo(0).WithMessage("O campo Profile não é valido")
                .LessThanOrEqualTo(2).WithMessage("O campo Profile não é valido");

            RuleFor(x => x.RG)
                .Length(1, 15).WithMessage("O campo Document não é um RG Valido")
                .Must(y => Regex.IsMatch(y, "^[0-9a-zA-Z]+$"))
                    .WithMessage($"O campo Document não é um RG Valido");

            RuleFor(x => x.Username)
                .SetValidator(new RequiredFieldValidator<string>("Username"));
        }
    }
}
