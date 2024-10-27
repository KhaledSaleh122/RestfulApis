using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RestfulApis_Application.CurrencySpace;
using RestfulApis_Application.TopicSpace;
using RestfulApis_Application.User;
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
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<UserHandlers>(sp =>
                new UserHandlers(
                    sp.GetRequiredService<IUserRepository>(),
                    sp.GetRequiredService<ITokenService>()
                )
            );
            services.AddScoped<TopicHandlers>(sp =>
                new TopicHandlers(
                    sp.GetRequiredService<ITopicRepository>(),
                    sp.GetRequiredService<IValidator<TopicDto>>()
                )
            );

            services.AddScoped<HttpClient>(sp => new HttpClient());

            services.AddScoped<CurrencyHandlers>(sp =>
                new CurrencyHandlers(
                    sp.GetRequiredService<ICurrencyRepository>()
                )
            );
            return services;
        }
    }
}
