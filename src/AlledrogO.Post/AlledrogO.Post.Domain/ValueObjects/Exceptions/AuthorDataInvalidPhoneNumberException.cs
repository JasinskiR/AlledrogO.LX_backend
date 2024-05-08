using AlledrogO.Shared.Domain.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class AuthorDataInvalidPhoneNumberException : AuthorException
{
    public AuthorDataInvalidPhoneNumberException(string phoneNumber) : base($"Invalid phone number: {phoneNumber}")
    {
    }
}