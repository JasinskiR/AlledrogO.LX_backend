using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class PostWithSameTitleAlreadyExistsException : AlledrogoException
{
    public PostWithSameTitleAlreadyExistsException(string title) : base($"Post with title '{title}' already exists")
    {
    }
}