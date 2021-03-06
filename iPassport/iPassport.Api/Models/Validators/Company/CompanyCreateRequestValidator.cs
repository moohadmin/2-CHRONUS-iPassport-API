using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using iPassport.Domain.Utils;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Plans
{
    public class CompanyCreateRequestValidator : AbstractValidator<CompanyCreateRequest>
    {
        public CompanyCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.Name)
                .SetValidator(new RequiredFieldValidator<string>("Name", localizer));

            RuleFor(s => s.Cnpj)
                .SetValidator(new RequiredFieldValidator<string>("Cnpj", localizer))
                 .Must(x => CnpjUtils.Valid(x))
                        .WithMessage(string.Format(localizer["InvalidField"], "Cnpj"));

            RuleFor(s => s.Address)
                .SetValidator(new RequiredFieldValidator<AddressCreateRequest>("Address", localizer))
                .SetValidator(new AddressValidator(localizer));
        }
    }
}
