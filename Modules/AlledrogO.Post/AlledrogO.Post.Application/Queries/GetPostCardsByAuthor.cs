using AlledrogO.Post.Application.DTOs;
using AlledrogO.Shared.Queries;

namespace AlledrogO.Post.Application.Queries;

public record GetPostCardsByAuthor(Guid AuthorId) : IQuery<IEnumerable<PostCardDto>>;