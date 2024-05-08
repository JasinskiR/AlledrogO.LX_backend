namespace AlledrogO.Shared.Domain.Exceptions;

public abstract class PostException : Exception
{
    protected PostException(string message) : base(message)
    {
    }
}