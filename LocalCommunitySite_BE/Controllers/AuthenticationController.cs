using AutoMapper;
using LocalCommunitySite.API.Configuration;
using LocalCommunitySite.API.Extentions.Exceptions;
using LocalCommunitySite.API.Models.AuthenticationDtos;
using LocalCommunitySite.API.Models.UserDtos;
using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParameters,
            AppDbContext appDbContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenValidationParameters = tokenValidationParameters;
            _appDbContext = appDbContext;
            _mapper = mapper;
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

            var jwtToken = await GenerateJwtToken(newUser);

            return Ok(jwtToken);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UserEditDto source)
        {
            var user = await _userManager.FindByEmailAsync(source.Email);

            if (user != null)
            {
                throw new BadRequestException("User with such email is already exist");
            }

            await _userManager.ChangePasswordAsync(user, source.CurrentPassword, source.NewPassword);

            return Ok();
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                throw new BadRequestException("User with such email is already exist");
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            return Ok(users.Select(x => _mapper.Map<UserDto>(x)));
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                throw new BadRequestException("User with such email is already exist");
            }

            await _userManager.DeleteAsync(user);

            return Ok();
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

            var jwtToken = await GenerateJwtToken(existingUser);

            return Ok(jwtToken);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            var result = await VerifyAndGenerateToken(tokenRequest);

            if(result == null)
            {
                throw new BadRequestException("Invalid tokens");
            }

            return Ok(result);
        }

        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // validation 1
            var tokenVerification = jwtTokenHandler
                .ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

            //validation 2
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture);

                if (result == false)
                {
                    return null;
                }
            }

            //validation 3
            var utcExpirationDate = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expirationDate = UnixTimeStampToDateTime(utcExpirationDate);

            if(expirationDate > DateTime.UtcNow)
            {
                return new AuthResult()
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        "Token has not yet expired"
                    }
                };
            }

            //validation 4
            var storedToken = await _appDbContext.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

            if(storedToken == null)
            {
                return new AuthResult()
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        "Token does not exists"
                    }
                };
            }

            //Validation 5
            if(storedToken.IsUsed)
            {
                return new AuthResult()
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        "Token has been used"
                    }
                };
            }

            //Validation 6
            if (storedToken.IsRevoked)
            {
                return new AuthResult()
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        "Token has been revoked"
                    }
                };
            }

            //Validation 7
            var jti = tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            if (storedToken.JwtId != jti)
            {
                return new AuthResult()
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        "Token does not match"
                    }
                };
            }

            storedToken.IsUsed = true;

            await _appDbContext.SaveChangesAsync();

            var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);

            return await GenerateJwtToken(dbUser);
        }

        private DateTime UnixTimeStampToDateTime(long utcExpirationDate)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(utcExpirationDate);

            return dateTimeVal;
        }

        private async Task<AuthResult> GenerateJwtToken(IdentityUser user)
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
                Expires = DateTime.UtcNow.AddMinutes(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMonths(6),
                Token = RandomString(35) + Guid.NewGuid()
            };

            await _appDbContext.RefreshTokens.AddAsync(refreshToken);

            await _appDbContext.SaveChangesAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                IsSuccess = true,
                RefreshToken = refreshToken.Token
            };
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)])
                .ToArray());

        }
    }
}
