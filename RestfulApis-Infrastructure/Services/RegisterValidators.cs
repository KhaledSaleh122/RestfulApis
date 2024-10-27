using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RestfulApis_Application.TopicSpace;

namespace RestfulApis_Infrastructure.Services
{
    public static class RegisterValidators
    {
        public static IServiceCollection AddValidators(this IServiceCollection services) {
            services.AddScoped<IValidator<TopicDto>, TopicValidator>();
            return services;
        }
    }
}
