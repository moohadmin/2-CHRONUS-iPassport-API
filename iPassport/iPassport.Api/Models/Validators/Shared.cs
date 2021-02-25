using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;

namespace iPassport.Api.Models.Validators
{
    public class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator()
        {
            RuleFor(x => x)
                .SetValidator(new RequiredFieldValidator<string>("E-mail"))
                .EmailAddress().WithMessage("E-mail inválido");
        }
    }

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

    public class ImageFileValidator : AbstractValidator<IFormFile>
    {
        public ImageFileValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(10000000)
                .WithMessage("Tamanho do arquivo de imagem maior que o máximo permitido: 10mb");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("Formato do arquivo de imagem não suportado. Formatos aceitos: jpeg, jpg, png");

        }
    }

    public class LatitudeValidator : AbstractValidator<double>
    {
        public LatitudeValidator() { }
        public LatitudeValidator(string fieldName)
        {
            RuleFor(x => x)
                .NotNull().WithMessage($"O campo {fieldName} é obrigatório")
                .LessThanOrEqualTo(90).WithMessage($"O campo {fieldName} deve está entre -90 a 90")
                .GreaterThanOrEqualTo(-90).WithMessage($"O campo {fieldName} deve está entre -90 a 90");
        }
    }

    public class LongitudeValidator : AbstractValidator<double>
    {
        public LongitudeValidator() { }
        public LongitudeValidator(string fieldName)
        {
            RuleFor(x => x)                
                .NotNull().WithMessage($"O campo {fieldName} é obrigatório")
                .LessThanOrEqualTo(180).WithMessage($"O campo {fieldName} deve está entre -180 a 180")
                .GreaterThanOrEqualTo(-180).WithMessage($"O campo {fieldName}  deve está entre -180 a 180");
        }
    }
}
