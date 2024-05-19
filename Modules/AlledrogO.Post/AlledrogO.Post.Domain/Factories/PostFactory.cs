using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.ValueObjects;

namespace AlledrogO.Post.Domain.Factories;

public class PostFactory : IPostFactory
{
    public Entities.Post Create(Guid id, PostTitle title, PostDescription description, Author author)
    {
        // Here could be policies for creating a post
        return new Entities.Post(id, title, description, author, author.AuthorDetails);
    }

    public Entities.Post CreateWithCustomDetails(Guid id, PostTitle title, PostDescription description, Author author, AuthorDetails authorDetails)
    {
        // Here could be policies for creating a post
        return new Entities.Post(id, title, description, author, authorDetails);
    }
}