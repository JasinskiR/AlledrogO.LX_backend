using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class GetPostCardsByAuthorHandler : IQueryHandler<GetPostCardsByAuthor, IEnumerable<PostCardDto>>
{
    private readonly DbSet<PostDbModel> _posts;

    public GetPostCardsByAuthorHandler(ReadDbContext context)
    {
        _posts = context.Set<PostDbModel>();
    }
    public async Task<IEnumerable<PostCardDto>> HandleAsync(GetPostCardsByAuthor query)
    {
        var posts = await _posts
            .Where(p => p.Author.Id == query.AuthorId)
            .Include(p => p.Images)
            .Select(p => p.AsCardDto())
            .AsNoTracking()
            .ToListAsync();
        return posts;
    }
}