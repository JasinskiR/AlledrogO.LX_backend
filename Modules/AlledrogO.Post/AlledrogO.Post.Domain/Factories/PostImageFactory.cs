using AlledrogO.Post.Domain.Entities;

namespace AlledrogO.Post.Domain.Factories;

public class PostImageFactory : IPostImageFactory
{
    public PostImage Create(Guid id, Entities.Post post, string url)
    {
        return new PostImage(id, post, url, false);
    }
}