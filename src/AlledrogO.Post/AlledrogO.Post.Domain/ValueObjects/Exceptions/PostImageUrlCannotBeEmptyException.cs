using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class PostImageUrlCannotBeEmptyException : AlledrogoException
{
    public PostImageUrlCannotBeEmptyException() : base("Post image URL cannot be empty")
    {
    }
}