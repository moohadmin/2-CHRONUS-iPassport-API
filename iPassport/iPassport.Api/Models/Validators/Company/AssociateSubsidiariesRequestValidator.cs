using FluentValidation;
using iPassport.Api.Models.Requests.Company;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace iPassport.Api.Models.Validators.Company
{
    /// <summary>
    /// Associate Subsidiaries Request Validator
    /// </summary>
    public class AssociateSubsidiariesRequestValidator : AbstractValidator<AssociateSubsidiariesRequest>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="localizer">Localizer</param>
        public AssociateSubsidiariesRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.AssociateAll)
                .Must(x => x != null)
                .When(x => x.Subsidiaries == null || !x.Subsidiaries.Any())
                .WithMessage(string.Format(localizer["RequiredField"], localizer["AssociateAll"]));

            RuleFor(x => x.AssociateAll)
                .Must(x => x == null || x == false)
                .When(x => x.Subsidiaries != null && x.Subsidiaries.Any())
                .WithMessage(localizer["AssociateAllMustBeNullWhenSubsidiariesIsNotNull"]);

            RuleFor(x => x.Subsidiaries)
                .Must(x => x != null && x.Any())
                .When(x => x.AssociateAll == null || x.AssociateAll == false)
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Subsidiaries"]));
        }
    }
}
