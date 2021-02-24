using FluentValidation;
using iPassport.Api.Models.Requests;

namespace iPassport.Api.Models.Validators.Users
{
    public class UserImageRequestValidator : AbstractValidator<UserImageRequest>
    {
        public UserImageRequestValidator()
        {
            RuleFor(x => x.ImageFile)
                .SetValidator(new ImageFileValidator());            
        }
    }
}
