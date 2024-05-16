using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.ValueObjects;

namespace AlledrogO.Post.Domain.Factories;

public interface ITagFactory
{
    Tag CreateNew(TagName name, Entities.Post post);
    Tag Create(Guid id, TagName name, IEnumerable<Entities.Post> posts);
}