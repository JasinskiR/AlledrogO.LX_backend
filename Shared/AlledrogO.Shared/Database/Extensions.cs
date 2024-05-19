using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace AlledrogO.Shared.Database;

public static class Extensions
{
    private const string sectionName = "Postgres";
    
    internal static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(sectionName)
            ?? throw new NullReferenceException("Postgres options not found");
        
        services.AddHostedService<DatabaseInitializer>();
        services.Configure<PostgresOptions>(options => section.Bind(options));
        
        return services;
    }
    
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration[$"{sectionName}:{nameof(PostgresOptions.ConnectionString)}"];
        services.AddDbContext<T>(x => x.UseNpgsql(connectionString));

        return services;
    }
}