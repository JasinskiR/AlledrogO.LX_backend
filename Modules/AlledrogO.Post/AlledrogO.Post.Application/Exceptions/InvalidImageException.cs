using AlledrogO.Shared.Exceptions;
using FluentValidation.Results;

namespace AlledrogO.Post.Application.Exceptions;

public class InvalidImageException : AlledrogoException
{
    public InvalidImageException(IEnumerable<ValidationFailure> errors) 
        : base(string.Join("  ", errors))
    {
    }
}