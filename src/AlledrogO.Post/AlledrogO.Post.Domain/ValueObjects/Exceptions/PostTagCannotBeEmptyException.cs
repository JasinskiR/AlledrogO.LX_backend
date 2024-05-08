using AlledrogO.Shared.Domain.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class PostTagCannotBeEmptyException : PostException
{
    public PostTagCannotBeEmptyException() : base("Post tag cannot be empty")
    {
    }
}