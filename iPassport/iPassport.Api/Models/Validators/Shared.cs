using FluentValidation;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System;

namespace iPassport.Api.Models.Validators
{
    public class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator() { }
        public GuidValidator(string fieldName, IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x)
                .SetValidator(new RequiredFieldValidator<Guid>(fieldName, localizer))
                .Must(g => Guid.TryParse(g.ToString(), out g)).WithMessage(localizer["InvalidId"]);
        }
    }

    public class RequiredFieldValidator<T> : AbstractValidator<T>
    {
        public RequiredFieldValidator(string fieldName, IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x)
                .NotNull().WithMessage(string.Format(localizer["RequiredField"], fieldName))
                .NotEmpty().WithMessage(string.Format(localizer["RequiredField"], fieldName));
        }
    }
}
