using AlledrogO.Post.Infrastructure.EF;
using AlledrogO.Shared;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Post.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDatabase();
        return services;
    }
}