using AlledrogO.Shared.Domain.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class AuthorDataInvalidEmailException : AuthorException
{
    public AuthorDataInvalidEmailException(string email) : base($"Author email '{email}' is invalid")
    {
    }
}