namespace AlledrogO.Shared.Exceptions;

public abstract class AlledrogoException : Exception
{
    protected AlledrogoException(string message) : base(message)
    {
    }
}