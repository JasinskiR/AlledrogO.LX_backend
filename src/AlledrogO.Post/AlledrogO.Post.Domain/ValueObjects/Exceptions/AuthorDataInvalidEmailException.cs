using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class AuthorDataInvalidEmailException : AlledrogoException
{
    public AuthorDataInvalidEmailException(string email) : base($"Author email '{email}' is invalid")
    {
    }
}