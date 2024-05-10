using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Post;

public record PostArchivedDE(Entities.Post Post) : IDomainEvent;