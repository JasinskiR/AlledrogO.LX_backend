using AlledrogO.Shared.Domain.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class PostImageUrlCannotBeEmptyException : PostException
{
    public PostImageUrlCannotBeEmptyException() : base("Post image URL cannot be empty")
    {
    }
}