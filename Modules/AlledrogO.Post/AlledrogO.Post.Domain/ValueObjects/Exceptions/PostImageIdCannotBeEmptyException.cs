using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class PostImageIdCannotBeEmptyException : AlledrogoException
{
    public PostImageIdCannotBeEmptyException() : base("Post image id cannot be empty.")
    {
    }
}