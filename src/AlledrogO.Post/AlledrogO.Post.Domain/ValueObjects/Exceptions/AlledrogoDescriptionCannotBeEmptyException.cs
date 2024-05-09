using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

internal class AlledrogoDescriptionCannotBeEmptyException : AlledrogoException
{
    public AlledrogoDescriptionCannotBeEmptyException() : base("Post description cannot be empty")
    {
    }
}