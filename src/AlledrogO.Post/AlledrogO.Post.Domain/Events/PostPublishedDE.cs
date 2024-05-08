using AlledrogO.Post.Domain.Entities;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events;

public record PostPublishedDE(Entities.Post Post) : IDomainEvent;