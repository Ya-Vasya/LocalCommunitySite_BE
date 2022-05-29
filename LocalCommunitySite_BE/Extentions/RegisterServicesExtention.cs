using FluentValidation;
using LocalCommunitySite.API.Models.AuthenticationDtos;
using LocalCommunitySite.API.Services;
using LocalCommunitySite.API.Services.Interfaces;
using LocalCommunitySite.API.Validators.AuthenticationValidators;
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
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<ICommentService, CommentService>();

            //repositories
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            //validators
            services.AddTransient<IValidator<UserRegistrationDto>, UserRegistrationDtoValidator>();
        }
    }
}
