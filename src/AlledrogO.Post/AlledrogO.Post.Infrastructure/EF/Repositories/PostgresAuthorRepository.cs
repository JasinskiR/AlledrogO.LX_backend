using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.EF.Repositories;

public class PostgresAuthorRepository : IAuthorRepository
{
    private readonly DbSet<Author> _authors;
    private readonly WriteDbContext _dbContext;

    public PostgresAuthorRepository(WriteDbContext dbContext)
    {
        _authors = dbContext.Authors;
        _dbContext = dbContext;
    }

    public Task<Author> GetAsync(Guid id)
    {
        return _authors
            .SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Author author)
    {
        await _authors.AddAsync(author);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author author)
    {
        _authors.Update(author);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Author author)
    {
        _authors.Remove(author);
        await _dbContext.SaveChangesAsync();
    }
}