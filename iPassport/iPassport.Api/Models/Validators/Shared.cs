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
        public GuidValidator(string fieldName)
        {
            RuleFor(x => x)
                .SetValidator(new RequiredFieldValidator<Guid>(string.IsNullOrWhiteSpace(fieldName) ? "Id" : fieldName))
                .Must(g => Guid.TryParse(g.ToString(), out g)).WithMessage("Identificador inválido");
        }
    }

    public class RequiredFieldValidator<T> : AbstractValidator<T>
    {
        public RequiredFieldValidator(string fieldName)
        {
            RuleFor(x => x)
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
}
