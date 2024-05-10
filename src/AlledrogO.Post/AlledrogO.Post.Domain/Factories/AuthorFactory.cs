using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.ValueObjects;

namespace AlledrogO.Post.Domain.Factories;

public class AuthorFactory : IAuthorFactory
{
    public Author Create(Guid id, AuthorDetails authorDetails, List<Entities.Post> posts)
    {
        return new Author(id, authorDetails, posts);
    }
    
}