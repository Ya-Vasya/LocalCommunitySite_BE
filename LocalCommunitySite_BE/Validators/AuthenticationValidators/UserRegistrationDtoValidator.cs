using FluentValidation;
using LocalCommunitySite.API.Models.AuthenticationDtos;

namespace LocalCommunitySite.API.Validators.AuthenticationValidators
{
    public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
    {
        public UserRegistrationDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(255);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(255);
        }
    }
}
