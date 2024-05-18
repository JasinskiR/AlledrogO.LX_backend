using AlledrogO.Post.Application.Services;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Shared;
using AlledrogO.Shared.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Post.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        services.AddCommands();
        services.AddSingleton<IPostFactory, PostFactory>();
        services.AddSingleton<ITagFactory, TagFactory>();
        services.AddSingleton<IAuthorFactory, AuthorFactory>();
        services.AddScoped<IAuthorPermissionService, AuthorPermissionService>();
        // here could be scanning for policies
        return services;
    }
}