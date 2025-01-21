using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace AlledrogO.Shared.Database;

public static class Extensions
{
    private const string SectionName = "Postgres";
    private const string EnvironmentVariableName = "DATABASE_CONNECTION_STRING";
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
        // var connectionString = configuration.GetSection("Postgres")["ConnectionString"];
        var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariableName);
        services.AddDbContext<T>(x => x.UseNpgsql(connectionString));

        return services;
    }
}