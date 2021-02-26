using FluentValidation;
using iPassport.Domain.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators
{
    //public class EmailValidator : AbstractValidator<string>
    //{
    //    public EmailValidator()
    //    {
    //        RuleFor(x => x)
    //            .SetValidator(new RequiredFieldValidator<string>("E-mail"))
    //            .EmailAddress().WithMessage("E-mail inválido");
    //    }
    //}

    public class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator() { }
        public GuidValidator(string fieldName)
        {
            RuleFor(x => x)
                .SetValidator(new RequiredFieldValidator<Guid>(fieldName))
                .Must(g => Guid.TryParse(g.ToString(), out g)).WithMessage("Identificador inválido");
        }
    }

    public class RequiredFieldValidator<T> : AbstractValidator<T>
    {
        public RequiredFieldValidator(string fieldName)
        {
            RuleFor(x => x)
                .NotNull().WithMessage($"O campo {fieldName} é obrigatório")
                .NotEmpty().WithMessage($"O campo {fieldName} é obrigatório");
        }
    }

    //public class ImageFileValidator : AbstractValidator<IFormFile>
    //{
    //    public ImageFileValidator()
    //    {
    //        RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(10000000)
    //            .WithMessage("Tamanho do arquivo de imagem maior que o máximo permitido: 10mb");

    //        RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
    //            .WithMessage("Formato do arquivo de imagem não suportado. Formatos aceitos: jpeg, jpg, png");

    //    }
    //}

    //public class PhoneNumberValidator : AbstractValidator<string>
    //{
    //    public PhoneNumberValidator()
    //    {
    //        RuleFor(x => x).Cascade(CascadeMode.Stop)
    //            .NotEmpty()
    //                .WithMessage("O número de telefone informado não é válido. Por favor, verifique")
    //            .Length(13)
    //                .WithMessage("O número de telefone informado não é válido. Por favor, verifique")                
    //            .Must(y => y.Substring(4, 1).Equals("9"))
    //                .WithMessage("A informação inserida para o nono dígito não é válida.");

    //        RuleFor(x => x).Must(y => Regex.IsMatch(y,"^[0-9]+$")).WithMessage("O número de telefone informado não é válido. Por favor, verifique");
    //    }
    //}

    //public class LatitudeValidator : AbstractValidator<double>
    //{
    //    public LatitudeValidator() { }
    //    public LatitudeValidator(string fieldName)
    //    {
    //        RuleFor(x => x)
    //            .NotNull().WithMessage($"O campo {fieldName} é obrigatório")
    //            .LessThanOrEqualTo(90).WithMessage($"O campo {fieldName} deve está entre -90 a 90")
    //            .GreaterThanOrEqualTo(-90).WithMessage($"O campo {fieldName} deve está entre -90 a 90");
    //    }
    //}

    //public class LongitudeValidator : AbstractValidator<double>
    //{
    //    public LongitudeValidator() { }
    //    public LongitudeValidator(string fieldName)
    //    {
    //        RuleFor(x => x)
    //            .NotNull().WithMessage($"O campo {fieldName} é obrigatório")
    //            .LessThanOrEqualTo(180).WithMessage($"O campo {fieldName} deve está entre -180 a 180")
    //            .GreaterThanOrEqualTo(-180).WithMessage($"O campo {fieldName}  deve está entre -180 a 180");
    //    }
    //}

    //public class CnsValidator : AbstractValidator<string>
    //{
    //    public CnsValidator() { }
    //    public CnsValidator(string fieldName)
    //    {
    //        RuleFor(x => x)
    //           .Length(15).WithMessage($"o campo {fieldName} não é um CNS Valido")
    //           .Must(y => Regex.IsMatch(y, "^[0-9]+$"))
    //           .WithMessage($"O campo {fieldName} não é um CNS Valido");
    //    }
    //}

    //public class CpfValidator : AbstractValidator<string>
    //{
    //    public CpfValidator() { }
    //    public CpfValidator(string fieldName)
    //    {
    //        RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
    //           .Length(11).WithMessage($"O campo Document não é um CPF Valido")
    //           .Must(x => Regex.IsMatch(x, "^[0-9]+$")).WithMessage($"O campo Document não é um CPF Valido")
    //           .Must(x => CpfVerification.Validar(x)).WithMessage($"O campo Document não é um CPF Valido");
    //    }
    //}

    //public class RgValidator : AbstractValidator<string>
    //{
    //    public RgValidator() { }
    //    public RgValidator(string fieldName)
    //    {
    //        RuleFor(x => x.Document)
    //           .Length(1,15).WithMessage("O campo Document não é um RG Valido")
    //           .Must(y => Regex.IsMatch(y, "^[0-9a-zA-Z]+$"))
    //           .WithMessage($"O campo Document não é um RG Valido");
    //    }
    //}

    //public class PassportValidator : AbstractValidator<string>
    //{
    //    public PassportValidator() { }
    //    public PassportValidator(string fieldName)
    //    {
    //        RuleFor(x => x.Document)
    //           .Length(3,15).WithMessage("O campo Document não é um Passaporte Valido")
    //           .Must(y => Regex.IsMatch(y, "^[a-zA-Z]{2}[0-9]+$")).WithMessage($"O campo Document não é um Passaporte Valido");
    //    }
    //}

}
