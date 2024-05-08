using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events;

public record PostArchivedDE(Entities.Post Post) : IDomainEvent;