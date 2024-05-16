using AlledrogO.Post.Application.DTOs;
using AlledrogO.Shared.Queries;

namespace AlledrogO.Post.Application.Queries;

public record SearchPostsByAuthor(Guid AuthorId) : IQuery<IEnumerable<PostDto>>;