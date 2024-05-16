using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Shared.Modules;

public static class Extensions
{
    public static IServiceCollection AddModuleRequests(this IServiceCollection services)
    {
        services.AddSingleton<IModuleRegistry, ModuleRegistry>();
        services.AddSingleton<IModuleSubscriber, ModuleSubscriber>();
        return services;
    }
    
    public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
    {
        return app.ApplicationServices.GetRequiredService<IModuleSubscriber>();
    }
}