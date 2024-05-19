using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

internal class PostDescriptionCannotBeEmptyException : AlledrogoException
{
    public PostDescriptionCannotBeEmptyException() : base("Post description cannot be empty")
    {
    }
}