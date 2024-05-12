using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.EF.Repositories;

internal sealed class PostgresPostRepository : IPostRepository
{
    private readonly DbSet<Domain.Entities.Post> _posts;
    private readonly WriteDbContext _dbContext;

    public PostgresPostRepository(WriteDbContext dbContext)
    {
        _posts = dbContext.Posts;
        _dbContext = dbContext;
    }

    public Task<Domain.Entities.Post> GetAsync(Guid id)
    {
        return _posts
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .Include(p => p.Images)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Domain.Entities.Post post)
    {
        await _posts.AddAsync(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entities.Post post)
    {
        _posts.Update(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Domain.Entities.Post post)
    {
        _posts.Remove(post);
        await _dbContext.SaveChangesAsync();
    }
}