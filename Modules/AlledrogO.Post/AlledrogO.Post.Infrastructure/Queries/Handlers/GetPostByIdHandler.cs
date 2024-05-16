using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class GetPostByIdHandler : IQueryHandler<GetPostById, PostDto>
{
    private readonly DbSet<PostDbModel> _posts;

    public GetPostByIdHandler(ReadDbContext dbContext)
    {
        _posts = dbContext.Set<PostDbModel>();
    }

    public async Task<PostDto> HandleAsync(GetPostById query)
    {
        var post = await _posts
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .Include(p => p.Images)
            .Where(p => p.Id == query.Id)
            .Select(p => p.AsDto())
            .AsNoTracking()
            .SingleOrDefaultAsync();
        return post;
    }
}