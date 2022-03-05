using FluentValidation;
using LocalCommunitySite.API.Models.AuthenticationDtos;
using LocalCommunitySite.API.Validators.AuthenticationValidators;
using Microsoft.Extensions.DependencyInjection;

namespace LocalCommunitySite.API.Extentions
{
    public static class RegisterServicesExtention
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // services
            //services.AddScoped<IPostService, PostService>();

            //repositories
            //services.AddScoped<IPostRepository, PostRepository>();

            //validators
            services.AddTransient<IValidator<UserRegistrationDto>, UserRegistrationDtoValidator>();
        }
    }
}
