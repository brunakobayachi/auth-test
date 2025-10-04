using NSwag;
using FastEndpoints.Swagger;

namespace Configs;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.SwaggerDocument(document =>
        {
            document.DocumentSettings = s =>
            {
                s.AddAuth("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Scheme = "basic",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.Basic,
                    Description = "Basic authentication using username and password"
                });
            };
        });

        return services;
    }
}
