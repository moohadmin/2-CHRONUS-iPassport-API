using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Text.RegularExpressions;

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

    /// <summary>
    /// AddressValidator Class
    /// </summary>
    public class AddressValidator : AbstractValidator<AddressCreateRequest>
    {
        /// <summary>
        /// AddressValidator Construtor
        /// </summary>
        public AddressValidator() { }

        /// <summary>
        /// AddressValidator Construtor
        /// </summary>
        public AddressValidator(IStringLocalizer<Resource> localizer, bool validateCep)
        {
            if (validateCep)
            {
                RuleFor(x => x.Cep).Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage(string.Format(localizer["InvalidField"], "Cep"))
                    .SetValidator(new RequiredFieldValidator<string>("Cep", localizer))
                    .Must(x => Regex.IsMatch(x, "^[0-9]{8}$")).WithMessage(string.Format(localizer["InvalidField"], "Cep"));

                RuleFor(x => x.Description).SetValidator(new RequiredFieldValidator<string>("Description", localizer));
            }

            RuleFor(y => y.Number)
                    .Must(x => Regex.IsMatch(x, "^[0-9]+$"))
                    .When(x => !String.IsNullOrWhiteSpace(x.Number))
                    .WithMessage(string.Format(localizer["InvalidField"], "Number"));

            RuleFor(x => x.CityId)
                .SetValidator(new GuidValidator("CityId", localizer));
        }
    }

    /// <summary>
    /// AddressValidator Class
    /// </summary>
    public class AddressEditValidator : AbstractValidator<AddressEditRequest>
    {
        /// <summary>
        /// AddressValidator Construtor
        /// </summary>
        public AddressEditValidator() { }

        /// <summary>
        /// AddressValidator Construtor
        /// </summary>
        public AddressEditValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], "AddressId"));

            RuleFor(x => x.Cep)
                .Must(x => Regex.IsMatch(x, "^[0-9]{8}$"))
                .When(x => !string.IsNullOrWhiteSpace(x.Cep))
                .WithMessage(string.Format(localizer["InvalidField"], "Cep"));

            RuleFor(y => y.Number)
                    .Must(x => Regex.IsMatch(x, "^[0-9]+$"))
                    .When(x => !String.IsNullOrWhiteSpace(x.Number))
                    .WithMessage(string.Format(localizer["InvalidField"], "Number"));

            RuleFor(x => x.CityId)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], "CityId"));
        }
    }
}
