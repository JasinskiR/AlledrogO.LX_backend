using AlledrogO.Message.Core;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Message.Api;

public static class Extensions
{
    public static IServiceCollection AddMessageModule(this IServiceCollection services)
    {
        services.AddMessageCore();

        return services;
    }
}