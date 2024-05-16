using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Post;

public record PostDescriptionUpdatedDE(Entities.Post Post, PostDescription NewDescription) : IDomainEvent;