using FluentValidation;
using iPassport.Api.Models.Requests;
using Microsoft.AspNetCore.Http;

namespace iPassport.Api.Models.Validators.Users
{
    public class UserImageRequestValidator : AbstractValidator<UserImageRequest>
    {
        public UserImageRequestValidator()
        {
            RuleFor(x => x.ImageFile)
                .SetValidator(new RequiredFieldValidator<IFormFile>("ImageFile"));

            RuleFor(x => x.ImageFile.Length).LessThanOrEqualTo(10000000)
                .WithMessage("Tamanho do arquivo de imagem maior que o máximo permitido: 10mb");

            RuleFor(x => x.ImageFile.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("Formato do arquivo de imagem não suportado. Formatos aceitos: jpeg, jpg, png");
        }
    }
}
