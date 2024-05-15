using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class GetTagByNameHandler : IQueryHandler<GetTagByName, TagDetailsDto>
{
    private readonly DbSet<TagDbModel> _tags;

    public GetTagByNameHandler(ReadDbContext context)
    {
        _tags = context.Tags;
    }

    public async Task<TagDetailsDto> HandleAsync(GetTagByName query)
    {
        return await _tags
            .Where(t => t.Name == query.Name)
            .Include(t => t.Posts)
            .Select(t => t.AsDetailsDto())
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}