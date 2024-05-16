using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AlledrogO.Shared.Database;

public class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(DbContext).IsAssignableFrom(type) && !type.IsInterface && type != typeof(DbContext));
        
        using var scope = _serviceProvider.CreateScope();
        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService(dbContextType) as DbContext;
            // dbContext.Database.EnsureDeleted();
            // dbContext.Database.EnsureCreated();
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}