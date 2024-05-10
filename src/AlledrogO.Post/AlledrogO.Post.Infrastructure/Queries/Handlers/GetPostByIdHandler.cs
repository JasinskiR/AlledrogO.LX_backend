using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models.ReadModels;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class GetPostByIdHandler : IQueryHandler<GetPostById, PostDto>
{
    private readonly DbSet<PostReadDbModel> _posts;

    public GetPostByIdHandler(ReadDbContext dbContext)
    {
        _posts = dbContext.Set<PostReadDbModel>();
    }

    public Task<PostDto> HandleAsync(GetPostById query)
    {
        return _posts
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .Where(p => p.Id == query.Id)
            .Select(p => p.AsDto())
            .AsNoTracking()
            .SingleOrDefaultAsync();
    }
}