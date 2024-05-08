using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class PostTagAlreadyExistsException : AlledrogoException
{
    public PostTagAlreadyExistsException(string tag) : base($"Post tag '{tag}' already exists")
    {
    }
}