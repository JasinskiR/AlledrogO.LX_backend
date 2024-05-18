using AlledrogO.Post.Domain.Entities;

namespace AlledrogO.Post.Domain.Factories;

public interface IPostImageFactory
{
    PostImage Create(Guid id, Entities.Post post, string url);
}