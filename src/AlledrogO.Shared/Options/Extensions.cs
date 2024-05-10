using Microsoft.Extensions.Configuration;

namespace AlledrogO.Shared.Options;

public static class Extensions
{
    public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName) where TOptions : class, new()
    {
        var options = new TOptions();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}