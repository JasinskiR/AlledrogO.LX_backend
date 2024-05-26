using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace AlledrogO.Shared.Database;

public static class Extensions
{
    private const string SectionName = "Postgres";
    
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(options => 
            configuration.GetSection(SectionName).Bind(options));
        services.AddHostedService<DatabaseInitializer>();
        return services;
    }
    
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        // var connectionString = configuration[$"{SectionName}:{nameof(PostgresOptions.ConnectionString)}"];
        // var connectionString = "Host=postgres;Database=alledrogo;Username=postgres;Password=postgres";
        var connectionString = configuration.GetSection("Postgres")["ConnectionString"];
        services.AddDbContext<T>(x => x.UseNpgsql(connectionString));

        return services;
    }
}