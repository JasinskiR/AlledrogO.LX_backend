using AlledrogO.Shared.Domain.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class PostNotFoundException : AuthorException
{
    public PostNotFoundException(string postName) : base($"Post '{postName}' not found")
    {
    }
}