using AlledrogO.Post.Domain.Entities;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Post;

public record PostAddedDE(Entities.Author Author, Entities.Post Post) : IDomainEvent;