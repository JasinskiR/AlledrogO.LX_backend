using AlledrogO.Shared.Domain.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class PostTitleCannotBeEmptyException : PostException
{
    public PostTitleCannotBeEmptyException() : base("Post title cannot be empty")
    {
    }
}