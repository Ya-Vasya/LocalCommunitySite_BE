using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LocalCommunitySite.API.Extentions
{
    public static class JwtConfigurationExtention
    {
        public static AuthenticationBuilder AddJwtConfig(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            builder.AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)    ,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });

            return builder;
        }
    }
}
