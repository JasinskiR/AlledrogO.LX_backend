using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Author;

public record AuthorPostArchivedDE(Entities.Author Author, Entities.Post Post) : IDomainEvent;