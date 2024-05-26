using AlledrogO.Post.Domain.Entities;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Post;

public record PostImageSetAsMainDE(Entities.Post Post, PostImage Image) : IDomainEvent;
