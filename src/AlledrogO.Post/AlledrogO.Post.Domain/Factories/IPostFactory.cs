using AlledrogO.Post.Domain.ValueObjects;

namespace AlledrogO.Post.Domain.Factories;

public interface IPostFactory
{
    Entities.Post Create(PostTitle title);
}