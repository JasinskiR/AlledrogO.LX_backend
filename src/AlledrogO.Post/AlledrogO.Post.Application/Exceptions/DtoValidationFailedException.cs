using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Application.Exceptions;

public class DtoValidationFailedException : AlledrogoException
{
    public DtoValidationFailedException(string validationError) : base(validationError)
    {
    }
}