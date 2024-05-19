using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Application.Exceptions;

public class AuthorNotFoundException : AlledrogoException
{
    public AuthorNotFoundException(Guid id) : base($"Author with id '{id}' not found")
    {
    }
}