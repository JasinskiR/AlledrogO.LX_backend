using AlledrogO.Post.Domain.Entities;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events;

public record AuthorPostPublishedDE(Author Author, Entities.Post Post) : IDomainEvent;