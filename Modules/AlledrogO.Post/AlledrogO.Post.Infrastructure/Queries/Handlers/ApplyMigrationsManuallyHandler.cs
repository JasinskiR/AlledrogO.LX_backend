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
        await _context.Database.MigrateAsync();
        return true;
    }
}