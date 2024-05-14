using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class GetTagsHandler : IQueryHandler<GetTags, IEnumerable<TagDto>>
{
    private readonly DbSet<TagDbModel> _tags;

    public GetTagsHandler(ReadDbContext context)
    {
        _tags = context.Tags;
    }

    public async Task<IEnumerable<TagDto>> HandleAsync(GetTags query)
    {
        return await _tags
            .Include(_tags => _tags.Posts)
            .Select(t => t.AsDto())
            .AsNoTracking()
            .ToListAsync();
    }
}