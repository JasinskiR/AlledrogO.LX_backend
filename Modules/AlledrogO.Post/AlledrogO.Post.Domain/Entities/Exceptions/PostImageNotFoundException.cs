using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class PostImageNotFoundException : AlledrogoException
{
    public PostImageNotFoundException(string url) : base($"Post image with url '{url}' not found")
    {
    }
}