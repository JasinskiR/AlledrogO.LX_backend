using AlledrogO.Post.Domain.ValueObjects;

namespace AlledrogO.Post.Domain.Factories;

public interface IPostFactory
{
    Entities.Post Create(Guid id, PostTitle title, PostDescription description, Guid authorId, AuthorData authorData);
}