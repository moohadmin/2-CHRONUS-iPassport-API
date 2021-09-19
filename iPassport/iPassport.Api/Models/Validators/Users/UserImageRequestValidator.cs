using FluentValidation;
using iPassport.Api.Models.Requests;
using iPassport.Application.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace iPassport.Api.Models.Validators.Users
{
    /// <summary>
    /// User Image Request Validator
    /// </summary>
    public class UserImageRequestValidator : AbstractValidator<UserImageRequest>
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="localizer">localizer</param>
        public UserImageRequestValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.ImageFile)
                .SetValidator(new RequiredFieldValidator<IFormFile>("ImageFile", localizer));

            RuleFor(x => x.ImageFile.Length).LessThanOrEqualTo(10000000)
                .WithMessage(string.Format(localizer["ImageMaxSize"], 10));

            RuleFor(x => x.ImageFile.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage(localizer["ImageValidTypes"]);
        }
    }
}
