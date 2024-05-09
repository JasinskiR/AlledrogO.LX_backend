using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class PostNotFoundException : AlledrogoException
{
    public PostNotFoundException(string postName) : base($"Post '{postName}' not found")
    {
    }
}