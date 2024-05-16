using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.DTOs.External;
using AlledrogO.Shared.Queries;

namespace AlledrogO.Post.Application.Queries;

public record SearchPosts(PostSearchWithTagsDto Search) : IQuery<IEnumerable<PostCardDto>>;