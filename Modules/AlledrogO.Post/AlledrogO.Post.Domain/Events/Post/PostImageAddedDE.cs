using AlledrogO.Post.Domain.Entities;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Post;

public record PostImageAddedDE(Entities.Post Post, PostImage Image) : IDomainEvent;