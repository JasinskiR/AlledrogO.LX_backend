using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.ValueObjects;

namespace AlledrogO.Post.Domain.Factories;

public interface IAuthorFactory
{
    Author Create(Guid id, AuthorDetails authorDetails, List<Entities.Post> posts);
}