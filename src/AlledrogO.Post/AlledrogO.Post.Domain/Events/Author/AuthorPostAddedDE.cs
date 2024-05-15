using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Author;

public record AuthorPostAddedDE(Entities.Author Author, Entities.Post Post) : IDomainEvent;