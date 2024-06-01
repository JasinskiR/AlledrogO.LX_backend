using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace AlledrogO.Shared.Database;

internal class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;
    public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetSection("Postgres")["ConnectionString"];
            Console.WriteLine($"Connection string: {connectionString}");
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            Console.WriteLine("Connection to database succeeded!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection to database failed: {ex.Message}");
        }
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

        using var scope = _serviceProvider.CreateScope();
        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;
            if (dbContext is null)
            {
                continue;
            }
            // var dbConnection = dbContext.Database.GetDbConnection();
            // Console.WriteLine($"DbContext: {dbContextType.Name}, Connection String: {dbConnection.ConnectionString}");    
            _logger.LogInformation($"Running DB context: {dbContext.GetType().Name}...");
            try
            {
                await dbContext.Database.EnsureCreatedAsync();
                await dbContext.Database.MigrateAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}