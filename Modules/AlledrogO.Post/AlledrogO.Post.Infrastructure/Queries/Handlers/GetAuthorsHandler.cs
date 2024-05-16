using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class GetAuthorsHandler : IQueryHandler<GetAuthors, IEnumerable<AuthorDto>>
{
    private readonly DbSet<AuthorDbModel> _authors;

    public GetAuthorsHandler(ReadDbContext context)
    {
        _authors = context.Set<AuthorDbModel>();
    }

    public async Task<IEnumerable<AuthorDto>> HandleAsync(GetAuthors query)
    {
        return await _authors
            .Select(a => a.AsDto())
            .AsNoTracking()
            .ToListAsync();
    }
}