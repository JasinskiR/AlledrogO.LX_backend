using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class PostTitleCannotBeEmptyException : AlledrogoException
{
    public PostTitleCannotBeEmptyException() : base("Post title cannot be empty")
    {
    }
}