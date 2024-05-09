using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.ValueObjects;

namespace AlledrogO.Post.Domain.Factories;

public interface IPostFactory
{
    Entities.Post Create(Guid id, PostTitle title, PostDescription description, Author author);
    Entities.Post CreateWithCustomDetails(Guid id, PostTitle title, PostDescription description, Author author, AuthorDetails authorDetails);
}