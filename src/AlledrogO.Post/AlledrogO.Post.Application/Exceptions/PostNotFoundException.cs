using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Application.Exceptions;

public class PostNotFoundException : AlledrogoException
{
    public PostNotFoundException(Guid id) : base($"Post with id '{id}' not found")
    {
    }
}