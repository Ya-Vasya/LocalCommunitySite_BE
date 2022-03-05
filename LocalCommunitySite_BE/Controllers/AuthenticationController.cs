using LocalCommunitySite.API.Extentions.Exceptions;
using LocalCommunitySite.API.Models.AuthenticationDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
        {
            var user = await _userManager.FindByEmailAsync(userRegistrationDto.Email);

            if(user != null)
            {
                throw new BadRequestException("User with such email is already exist");
            }

            IdentityUser newUser = new IdentityUser() { Email = userRegistrationDto.Email, UserName = userRegistrationDto.Email };
            var isCreated = await _userManager.CreateAsync(newUser, userRegistrationDto.Password);

            if(!isCreated.Succeeded)
            {
                throw new CustomException(isCreated.Errors.First().Description);
            }

            var jwtToken = GenerateJwtToken(newUser);

            return Ok(new RegistrationResponseDto()
            {
                IsSuccess = true,
                Token = jwtToken
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(userDto.Email);

            if(existingUser == null)
            {
                throw new BadRequestException("User with such email does not exist");
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, userDto.Password);

            if (!isCorrect)
            {
                throw new BadRequestException("Password is incorrect");
            }

            var jwtToken = GenerateJwtToken(existingUser);

            return Ok(new RegistrationResponseDto()
            {
                IsSuccess = true,
                Token = jwtToken
            });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
