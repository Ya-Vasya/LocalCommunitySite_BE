using FluentValidation;
using LocalCommunitySite.API.Models.AuthenticationDtos;

namespace LocalCommunitySite.API.Validators.AuthenticationValidators
{
    public class UserLoginRequestDtoValidator : AbstractValidator<UserLoginRequestDto>
    {
        public UserLoginRequestDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(255);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(255);
        }
    }
}
