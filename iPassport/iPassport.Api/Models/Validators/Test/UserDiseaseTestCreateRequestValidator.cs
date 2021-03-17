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
                .SetValidator(new RequiredFieldValidator<bool?>("TestDate", localizer))
                .When(x => x.WasTestPerformed.GetValueOrDefault()
                        && (x.Result.HasValue || x.ResultDate.HasValue));

            RuleFor(x => x.TestDate)
                .SetValidator(new RequiredFieldValidator<DateTime>("TestDate", localizer))
                .When(x => x.WasTestPerformed.GetValueOrDefault());

            RuleFor(x => x.ResultDate)
                .SetValidator(new RequiredFieldValidator<DateTime?>("ResultDate",localizer))
                .When(x => x.WasTestPerformed.GetValueOrDefault() && x.ResultDate.HasValue);
        }
    }
}
