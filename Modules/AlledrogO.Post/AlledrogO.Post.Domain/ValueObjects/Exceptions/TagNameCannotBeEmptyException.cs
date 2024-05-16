using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class TagNameCannotBeEmptyException : AlledrogoException
{
    public TagNameCannotBeEmptyException() : base("Tag name cannot be empty")
    {
    }
}