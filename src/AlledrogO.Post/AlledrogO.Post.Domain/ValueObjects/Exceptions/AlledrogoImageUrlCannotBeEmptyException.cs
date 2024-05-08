using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class AlledrogoImageUrlCannotBeEmptyException : AlledrogoException
{
    public AlledrogoImageUrlCannotBeEmptyException() : base("Post image URL cannot be empty")
    {
    }
}