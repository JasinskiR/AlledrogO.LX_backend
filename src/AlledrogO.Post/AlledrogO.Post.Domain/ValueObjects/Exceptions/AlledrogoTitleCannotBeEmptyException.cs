using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class AlledrogoTitleCannotBeEmptyException : AlledrogoException
{
    public AlledrogoTitleCannotBeEmptyException() : base("Post title cannot be empty")
    {
    }
}