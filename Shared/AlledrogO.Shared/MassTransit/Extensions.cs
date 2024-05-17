using AlledrogO.Shared.MassTransit.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Shared.MassTransit;

internal static class Extensions
{
    internal static IServiceCollection AddMessageBroker(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IMessageMarker).IsAssignableFrom(p)
                        && p.IsClass
                        && !p.IsAbstract)
            .Select(p => p.Assembly)
            .ToArray();
        services.AddMassTransit(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();
            configurator.UsingInMemory((context, config) => config.ConfigureEndpoints(context));
            configurator.AddConsumers(assemblies);
        });
        return services;
    }
}