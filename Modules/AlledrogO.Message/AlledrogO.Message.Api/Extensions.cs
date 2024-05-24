using AlledrogO.Message.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Message.Api;

public static class Extensions
{
    public static IServiceCollection AddMessageModule(this IServiceCollection services)
    {
        services.AddMessageCore();
        services.AddSignalR();
        return services;
    }
    
    public static IApplicationBuilder UseMessageModule(this IApplicationBuilder app)
    {
        
        return app;
    }
}