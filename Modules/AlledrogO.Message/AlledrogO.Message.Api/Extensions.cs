using AlledrogO.Message.Core;
using AlledrogO.Message.Core.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Message.Api;

public static class Extensions
{
    public static IServiceCollection AddMessageModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageCore(configuration);
        services.AddSignalR();
        return services;
    }
    
    public static IApplicationBuilder UseMessageModule(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ChatHub>("/chat");
        });
        return app;
    }
}