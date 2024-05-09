using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Post;

public record PostPublishedDE(Entities.Post Post) : IDomainEvent;