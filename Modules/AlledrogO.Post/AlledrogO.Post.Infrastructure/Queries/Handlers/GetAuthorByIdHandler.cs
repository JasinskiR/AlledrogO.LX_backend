using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.Queries.Handlers;

public class GetAuthorByIdHandler : IQueryHandler<GetAuthorById, AuthorDto>
{
    private readonly DbSet<AuthorDbModel> _authors;

    public GetAuthorByIdHandler(ReadDbContext context)
    {
        _authors = context.Set<AuthorDbModel>();
    }
    public async Task<AuthorDto> HandleAsync(GetAuthorById query)
    {
        return await _authors
            .Where(a => a.Id == query.Id)
            .Include(a => a.Posts)
            .Select(a => a.AsDto())
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}