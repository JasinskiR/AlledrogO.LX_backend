using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.EF.Repositories;

public class PostgresTagRepository : ITagRepository
{
    private readonly DbSet<Tag> _tags;
    private readonly WriteDbContext _dbContext;
    
    public PostgresTagRepository(WriteDbContext dbContext)
    {
        _tags = dbContext.Tags;
        _dbContext = dbContext;
    }
    
    public Task<Tag> GetAsync(Guid id)
    {
        return _tags
            .SingleOrDefaultAsync(t => t.Id == id);
    }

    public Task<Tag> GetAsync(TagName name)
    {
        return _tags
            .SingleOrDefaultAsync(t => t.Name == name);
    }

    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        return await _tags.ToListAsync();
    }

    public async Task AddAsync(Tag tag)
    {
        await _tags.AddAsync(tag);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tag tag)
    {
        _tags.Update(tag);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tag tag)
    {
        _tags.Remove(tag);
        await _dbContext.SaveChangesAsync();
    }
}