using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class ApplyMigrationsManuallyHandler : IQueryHandler<ApplyMigrationsManually, bool>
{
    private readonly ReadDbContext _context;

    public ApplyMigrationsManuallyHandler(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HandleAsync(ApplyMigrationsManually query)
    {
        // var migrations = _context.Database.GetMigrations();
        // foreach (var migration in migrations)
        // {
        //     await _context.Database.ExecuteSqlRawAsync($"DELETE FROM __EFMigrationsHistory WHERE MigrationId = '{migration}'");
        // }
        await _context.Database.MigrateAsync();
        return true;
    }
}