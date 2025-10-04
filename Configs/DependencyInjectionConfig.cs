using FastEndpointsBasicAuthEf.Services;
using Providers;

namespace Configs;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserAuthValidator, UserAuthValidator>();

        return services;
    } 
}