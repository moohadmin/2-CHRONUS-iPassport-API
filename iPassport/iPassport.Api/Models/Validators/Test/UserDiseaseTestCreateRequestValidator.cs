using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System;

namespace iPassport.Api.Models.Validators.Vaccines
{
    /// <summary>
    /// UserDiseaseTestCreateRequest Validator Class
    /// </summary>
    public class UserDiseaseTestCreateRequestValidator : AbstractValidator<UserDiseaseTestCreateRequest>
    {
        /// <summary>
        /// UserDiseaseTestCreateRequestValidator Contrutor
        /// </summary>
        /// <param name="localizer"> Resource</param>
        public UserDiseaseTestCreateRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Result)
                .NotNull()
                .When(x => (x.Result.HasValue || x.ResultDate.HasValue))
                .WithMessage(string.Format(localizer["RequiredField"], "Result"));

            RuleFor(x => x.TestDate)
                .SetValidator(new RequiredFieldValidator<DateTime>("TestDate", localizer));

            RuleFor(x => x.ResultDate)
                .NotEmpty()                
                .When(x => (x.Result.HasValue || x.ResultDate.HasValue))
                .WithMessage(string.Format(localizer["RequiredField"], "ResultDate"));
        }
    }
}
