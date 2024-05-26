using AlledrogO.Shared.Database;
using AlledrogO.User.Core.EF.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.User.Core.EF;

internal static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddPostgres<UserDbContext>();
        return services;
    }
}