using AlledrogO.Post.Application.DTOs;
using AlledrogO.Shared.Queries;

namespace AlledrogO.Post.Application.Queries;

public record GetTagById(Guid Id) : IQuery<TagDetailsDto>;