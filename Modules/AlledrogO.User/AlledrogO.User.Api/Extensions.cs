using AlledrogO.User.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.User.Api;

public static class Extensions
{
    public static IServiceCollection AddUserModule(this IServiceCollection services)
    {
        services.AddCore();
        return services;
    }

    public static IApplicationBuilder UseUserModule(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}