using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class PostTagNotFoundException : AlledrogoException
{
    public PostTagNotFoundException(string tag) : base($"Post tag '{tag}' not found")
    {
    }
}