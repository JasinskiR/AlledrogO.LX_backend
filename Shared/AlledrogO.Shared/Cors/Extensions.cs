using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlledrogO.Shared.Cors;

public static class Extensions
{
    public static IServiceCollection AddCorsForAngular(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedHosts = configuration.GetSection("AngularUrls").Get<string[]>()?.ToList()
            ?? throw new NullReferenceException("AllowedHosts not found in config file");
        
        var frontendIp = Environment.GetEnvironmentVariable("FRONTEND_IP");
        allowedHosts.Add($"https://{frontendIp}");
        allowedHosts.Add($"http://{frontendIp}");
        Console.WriteLine($"Allowed hosts: {string.Join(", ", allowedHosts)}");
        
        services.AddCors(
            options => options.AddPolicy(
                "Angular",
                policy => policy
                    .WithOrigins(allowedHosts.ToArray())
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
