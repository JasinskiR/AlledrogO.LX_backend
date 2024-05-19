using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Events.Post;

public record PostAuthorDetailsUpdatedDE(Entities.Post Post, AuthorDetails NewAuthorDetails) : IDomainEvent;