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
            RuleFor(x => x.CompanyTypeIdentifyer)
                .Must(x => x != null).WithMessage(string.Format(localizer["RequiredField"], localizer["CompanyType"]));

            RuleFor(x => x.SegmentIdentifyer)
                .Must(x => x != null).WithMessage(string.Format(localizer["RequiredField"], localizer["CompanySegmentType"]));

            RuleFor(x => x.LocalityId)
                .Must(x => x != null).WithMessage(string.Format(localizer["RequiredField"], localizer["Locality"])).When(x => x.CompanyTypeIdentifyer.HasValue && x.CompanyTypeIdentifyer.Value == ECompanyType.Government);

            RuleFor(x => x.Cnpj)
                .Must(x => !string.IsNullOrWhiteSpace(x) && x.Length == 8 && Regex.IsMatch(x, "^[0-9]+$")).When(x => x.CompanyTypeIdentifyer.HasValue && x.CompanyTypeIdentifyer.Value == ECompanyType.Private)
                .WithMessage(localizer["CnpjRequiredForPrivateCompany"]);        
        }
    }
}
