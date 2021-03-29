using FluentValidation;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System;

namespace iPassport.Api.Models.Validators.Vaccines
{
    /// <summary>
    /// User Disease Test Edit Request Validator Class
    /// </summary>
    public class UserDiseaseTestEditRequestValidator : AbstractValidator<UserDiseaseTestEditRequest>
    {
        /// <summary>
        /// User Disease Test Edit Request Validator Contrutor
        /// </summary>
        /// <param name="localizer">Resource</param>
        public UserDiseaseTestEditRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Result)
                .NotNull()
                .When(x => (x.Result.HasValue || x.ResultDate.HasValue))
                .WithMessage(string.Format(localizer["RequiredField"], localizer["TestResult"]));

            RuleFor(x => x.TestDate)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["TestDate"]))
                .LessThanOrEqualTo(DateTime.UtcNow).When(x => x.TestDate.HasValue).WithMessage(localizer["TestDateCannotBeHiggerThenActualDate"]);

            RuleFor(x => x.ResultDate)
                .NotEmpty()                
                .When(x => (x.Result.HasValue || x.ResultDate.HasValue))
                .WithMessage(string.Format(localizer["RequiredField"], localizer["ResultDate"]))
                .GreaterThanOrEqualTo(x => x.TestDate).When(x => x.ResultDate.HasValue).WithMessage(localizer["TestResultDateCannotBeHiggerThenTestDate"]);
        }
    }
}
