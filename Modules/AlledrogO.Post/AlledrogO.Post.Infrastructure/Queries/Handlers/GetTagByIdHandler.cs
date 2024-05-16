using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class GetTagByIdHandler : IQueryHandler<GetTagById, TagDetailsDto>
{
    private readonly DbSet<TagDbModel> _tags;

    public GetTagByIdHandler(ReadDbContext context)
    {
        _tags = context.Tags;
    }
    
    public async Task<TagDetailsDto> HandleAsync(GetTagById query)
    {
        return await _tags
            .Where(t => t.Id == query.Id)
            .Include(t => t.Posts)
            .Select(t => t.AsDetailsDto())
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}
