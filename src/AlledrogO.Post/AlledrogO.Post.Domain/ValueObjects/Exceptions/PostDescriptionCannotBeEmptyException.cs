using AlledrogO.Shared.Domain.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

internal class PostDescriptionCannotBeEmptyException : PostException
{
    public PostDescriptionCannotBeEmptyException() : base("Post description cannot be empty")
    {
    }
}