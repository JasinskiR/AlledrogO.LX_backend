using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class SearchPostsByAuthorHandler : IQueryHandler<SearchPostsByAuthor, IEnumerable<PostDto>>
{
    private readonly DbSet<PostDbModel> _posts;
    
    public SearchPostsByAuthorHandler(ReadDbContext dbContext)
    {
        _posts = dbContext.Set<PostDbModel>();
    }
    public async Task<IEnumerable<PostDto>> HandleAsync(SearchPostsByAuthor query)
    {
        return await _posts
            .Include(p => p.Tags)
            .Where(p => p.Author.Id == query.AuthorId)
            .Select(p => p.AsDto())
            .AsNoTracking()
            .ToListAsync();
    }
}