using FluentValidation;
using LocalCommunitySite.API.Models.AuthenticationDtos;

namespace LocalCommunitySite.API.Validators.AuthenticationValidators
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
