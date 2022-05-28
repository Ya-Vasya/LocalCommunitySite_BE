using FluentValidation;
using LocalCommunitySite.API.Models.AuthenticationDtos;
using LocalCommunitySite.API.Services;
using LocalCommunitySite.API.Validators.AuthenticationValidators;
using LocalCommunitySite.Domain.Interfaces.Services;
using LocalCommunitySite.Domain.Repositories;
using LocalCommunitySite.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LocalCommunitySite.API.Extentions
{
    public static class RegisterServicesExtention
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //services
            services.AddScoped<IPostService, PostService>();

            //repositories
            services.AddScoped<IPostRepository, PostRepository>();

            //validators
            services.AddTransient<IValidator<UserRegistrationDto>, UserRegistrationDtoValidator>();
        }
    }
}
