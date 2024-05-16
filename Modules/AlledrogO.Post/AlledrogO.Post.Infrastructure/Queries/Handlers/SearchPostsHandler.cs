using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.EF;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class SearchPostsHandler : IQueryHandler<SearchPosts, IEnumerable<PostCardDto>>
{
    private readonly DbSet<PostDbModel> _posts;
    
    public SearchPostsHandler(ReadDbContext dbContext)
    {
        _posts = dbContext.Set<PostDbModel>();
    }
    public async Task<IEnumerable<PostCardDto>> HandleAsync(SearchPosts query)
    {
        string searchString = query.Search.QueryString;
        List<string> tags = query.Search.Tags.ToList();
        
        var dbQuery = _posts
            .Include(p => p.Images)
            .Include(p => p.Tags)
            .AsQueryable();
        
        if (dbQuery is not null)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                dbQuery = dbQuery
                    .Where(p =>
                        Functions.ILike(p.Title, $"%{searchString}%") ||
                        Functions.ILike(p.Description, $"%{searchString}%"));
            }
            if (tags.Any())
            {
                dbQuery = dbQuery
                    .Where(p =>
                        p.Tags.Any(t => tags.Contains(t.Name)));
            }
        }
        return await dbQuery!
            .Select(p => p.AsCardDto())
            .AsNoTracking()
            .ToListAsync();
    }
}