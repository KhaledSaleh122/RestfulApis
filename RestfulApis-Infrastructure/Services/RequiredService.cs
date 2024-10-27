using Microsoft.Extensions.DependencyInjection;
using Restfulapis_Domain.Abstractions;
using RestfulApis_Infrastructure.Repositories;

namespace RestfulApis_Infrastructure.Services
{
    public static class RequiredService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            return services;
        }
    }
}
