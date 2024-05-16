using AlledrogO.Post.Application.DTOs;
using AlledrogO.Shared.Queries;

namespace AlledrogO.Post.Application.Queries;

public record GetAuthorById(Guid Id) : IQuery<AuthorDto>;