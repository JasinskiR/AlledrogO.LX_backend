using AlledrogO.Post.Domain.Entities;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Post;

public record PostImageRemovedDE(Entities.Post Post, PostImage Image) : IDomainEvent;