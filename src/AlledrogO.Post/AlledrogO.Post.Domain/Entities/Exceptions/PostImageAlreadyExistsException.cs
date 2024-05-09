using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class PostImageAlreadyExistsException : AlledrogoException
{
    public PostImageAlreadyExistsException(string url) : base($"Post image with url '{url}' already exists")
    {
    }
}