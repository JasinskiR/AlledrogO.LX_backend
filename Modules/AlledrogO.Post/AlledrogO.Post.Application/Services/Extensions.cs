using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Post.Application.Services;

public static class Extensions
{
    public static IServiceCollection AddImageService(this IServiceCollection services, IConfiguration configuration)
    {
        var imageSettings = configuration.GetSection("ImageSettings")
            ?? throw new NullReferenceException("No image settings found in config file");

        services.AddOptions<ImageServiceConfiguration>()
            .Bind(imageSettings)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddScoped<IImageService, ImageService>();
        return services;
    }
}