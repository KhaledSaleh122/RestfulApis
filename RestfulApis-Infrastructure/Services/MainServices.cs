using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RestfulApis_Infrastructure.Services
{
    public static class MainServices
    {
        public static IServiceCollection AddMainServices(this IServiceCollection services,IConfiguration configuration) {
            services.AddDBContextConfiguration(configuration);
            services.AddServices();
            return services;
        }
    }
}
