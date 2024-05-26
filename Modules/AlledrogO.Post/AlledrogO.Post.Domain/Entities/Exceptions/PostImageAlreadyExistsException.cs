using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class PostImageAlreadyExistsException : AlledrogoException
{
    public PostImageAlreadyExistsException(Guid id) : base($"Post image with id {id} already exists.")
    {
    }
}