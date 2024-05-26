using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class PostImageNotFoundException : AlledrogoException
{
    public PostImageNotFoundException(Guid id) : base($"Post image with id: '{id}' was not found.")
    {
    }
}