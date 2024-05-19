using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Post;

public record PostTitleUpdatedDE(Entities.Post Post, PostTitle NewTitle) : IDomainEvent;