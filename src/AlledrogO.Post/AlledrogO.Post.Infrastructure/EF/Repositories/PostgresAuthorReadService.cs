using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.EF.Repositories;

public class PostgresAuthorReadService : IAuthorReadService
{
    private readonly DbSet<AuthorReadDbModel> _authors;
    private readonly IAuthorFactory _authorFactory;
    private readonly IPostFactory _postFactory;
    public PostgresAuthorReadService(ReadDbContext dbContext, IAuthorFactory authorFactory, IPostFactory postFactory)
    {
        _authorFactory = authorFactory;
        _postFactory = postFactory;
        _authors = dbContext.Set<AuthorReadDbModel>();
    }
    
    public async Task<Author> GetAsync(Guid id)
    {
        var model = await _authors
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
        if (model == null)
        {
            return null;
        }

        var author = _authorFactory.Create(
            model.Id,
            new AuthorDetails(model.Details.Email, model.Details.PhoneNumber),
            model.Posts
                .Select(p => _postFactory.Create(p.Id, p.Title, p.Description, null))
                .ToList());
        return author;

    }
}