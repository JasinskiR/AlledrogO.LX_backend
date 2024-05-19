using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class GetPostCardsHandler : IQueryHandler<GetPostCards, IEnumerable<PostCardDto>>
{
    private readonly DbSet<PostDbModel> _posts;

    public GetPostCardsHandler(ReadDbContext context)
    {
        _posts = context.Set<PostDbModel>();
    }

    public async Task<IEnumerable<PostCardDto>> HandleAsync(GetPostCards query)
    {
        var posts = await _posts
            .Include(p => p.Images)
            .Select(p => p.AsCardDto())
            .AsNoTracking()
            .ToListAsync();
        return posts;
    }
}