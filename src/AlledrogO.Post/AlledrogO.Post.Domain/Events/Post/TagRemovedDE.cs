using AlledrogO.Post.Domain.Entities;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Post;

public record TagRemovedDE(Entities.Post Post, Tag Tag) : IDomainEvent;