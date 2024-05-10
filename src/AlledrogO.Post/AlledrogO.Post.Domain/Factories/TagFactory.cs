using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.ValueObjects;

namespace AlledrogO.Post.Domain.Factories;

public class TagFactory : ITagFactory
{
    public Tag CreateNew(TagName name, Entities.Post post)
    {
        return new Tag(new Guid(), name, new List<Entities.Post> { post });
    }

    public Tag Create(Guid id, TagName name, IEnumerable<Entities.Post> posts)
    {
        return new Tag(id, name, posts);
    }
}