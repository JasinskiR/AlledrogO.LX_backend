using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Application.Exceptions;

public class UnauthorizedEditException : AlledrogoException
{
    public UnauthorizedEditException() : base("You are not allowed to edit this post.")
    {
    }
}