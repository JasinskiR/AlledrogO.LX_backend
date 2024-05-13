using AlledrogO.Post.Application.DTOs;
using AlledrogO.Shared.Queries;

namespace AlledrogO.Post.Application.Queries;

public record GetAuthors() : IQuery<IEnumerable<AuthorDto>>;