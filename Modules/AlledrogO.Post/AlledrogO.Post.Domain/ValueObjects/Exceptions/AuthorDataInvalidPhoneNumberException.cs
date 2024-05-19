using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects.Exceptions;

public class AuthorDataInvalidPhoneNumberException : AlledrogoException
{
    public AuthorDataInvalidPhoneNumberException(string phoneNumber) : base($"Invalid phone number: {phoneNumber}")
    {
    }
}