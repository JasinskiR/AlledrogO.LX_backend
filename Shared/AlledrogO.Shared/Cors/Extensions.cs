using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Shared.Cors;

public static class Extensions
{
    public static IServiceCollection AddCorsForAngular(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedHosts = configuration.GetSection("AngularUrls").Get<string[]>()
            ?? throw new NullReferenceException("AllowedHosts not found in config file");
        services.AddCors(
            options => options.AddPolicy(
                "Angular",
                policy => policy
                    .WithOrigins(allowedHosts)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
        
        return services;
    }
    
    public static IApplicationBuilder UseCorsForAngular(this IApplicationBuilder app)
    {
        app.UseCors("Angular");
        return app;
    }
}