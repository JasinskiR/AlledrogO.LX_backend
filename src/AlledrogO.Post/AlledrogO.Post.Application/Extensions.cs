using AlledrogO.Post.Domain.Factories;
using AlledrogO.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Post.Application;

public static class Extensions
{
    public static IServiceCollection AddPostApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddSingleton<IPostFactory, PostFactory>();
        services.AddSingleton<ITagFactory, TagFactory>();
        
        // here could be scanning for policies
        return services;
    }
}