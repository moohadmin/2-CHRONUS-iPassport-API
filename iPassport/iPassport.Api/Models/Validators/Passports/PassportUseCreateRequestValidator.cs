using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Plans
{
    public class PassportUseCreateRequestValidator : AbstractValidator<PassportUseCreateRequest>
    {
        public PassportUseCreateRequestValidator()
        {
            RuleFor(x => x.Latitude)
                .NotNull().WithMessage("O campo Latitude é obrigatório")
                .LessThanOrEqualTo(90).WithMessage("O campo Latitude deve está entre -90 a 90")
                .GreaterThanOrEqualTo(-90).WithMessage("O campo Latitude deve está entre -90 a 90");

            RuleFor(x => x.Longitude)
                .NotNull().WithMessage("O campo Longitude é obrigatório")
                .LessThanOrEqualTo(180).WithMessage("O campo Longitude deve está entre -180 a 180")
                .GreaterThanOrEqualTo(-180).WithMessage("O campo Longitude deve está entre -180 a 180");

            RuleFor(s => s.PassportDetailsId)
                .SetValidator(new GuidValidator("PassportDetailsId"));
        }
    }
}
