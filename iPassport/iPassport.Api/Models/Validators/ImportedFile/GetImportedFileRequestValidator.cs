using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Indicators
{
    /// <summary>
    /// GetImportedFileRequestValidator Class
    /// </summary>
    public class GetImportedFileRequestValidator : AbstractValidator<GetImportedFileRequest>
    {
        /// <summary>
        /// GetImportedFileRequestValidator contructor
        /// </summary>
        /// <param name="localizer"> resource</param>
        public GetImportedFileRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(s => s.StartTime)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["StartTime"]));

            RuleFor(s => s.EndTime)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["EndTime"]));
        }
    }
}
