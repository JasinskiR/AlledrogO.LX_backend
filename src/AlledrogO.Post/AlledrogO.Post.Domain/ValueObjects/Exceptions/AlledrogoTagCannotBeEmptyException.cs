using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class AlledrogoTagCannotBeEmptyException : AlledrogoException
{
    public AlledrogoTagCannotBeEmptyException() : base("Post tag cannot be empty")
    {
    }
}