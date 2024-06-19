using AlledrogO.Message.Core.EF;
using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Database;
using AlledrogO.Shared.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Message.Core;

public static class Extensions
{
    public static IServiceCollection AddMessageCore(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddQueries();
        services.AddPostgres<MessageDbContext>();
        services.AddScoped<IChatUserRepository, ChatUserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();

        return services;
    }
}