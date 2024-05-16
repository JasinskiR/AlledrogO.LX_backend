using AlledrogO.Post.Application;
using AlledrogO.Post.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Post.Api;

public static class Extensions
{
    public static IServiceCollection AddPostModule(this IServiceCollection services,  IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);   
        return services;
    }
        
    public static IApplicationBuilder UsePostModule(this IApplicationBuilder app)
    {
        return app;
    }
}