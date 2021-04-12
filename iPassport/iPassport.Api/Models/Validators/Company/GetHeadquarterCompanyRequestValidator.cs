using FluentValidation;
using iPassport.Api.Models.Requests.Company;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using Microsoft.Extensions.Localization;
using System;
using System.Text.RegularExpressions;

namespace iPassport.Api.Models.Validators.Company
{
    /// <summary>
    /// Get Headquarter Company Request Validator
    /// </summary>
    public class GetHeadquarterCompanyRequestValidator : AbstractValidator<GetHeadquarterCompanyRequest>
    {

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">String localizer</param>
        public GetHeadquarterCompanyRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.CompanyTypeId)
                .Must(x => x != null).WithMessage(string.Format(localizer["RequiredField"], localizer["CompanyType"]));

            RuleFor(x => x.SegmentId)
                .Must(x => x != null).WithMessage(string.Format(localizer["RequiredField"], localizer["CompanySegmentType"]));       
        }
    }
}
