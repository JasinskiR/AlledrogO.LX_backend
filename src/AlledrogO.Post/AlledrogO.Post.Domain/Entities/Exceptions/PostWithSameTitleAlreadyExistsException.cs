using AlledrogO.Shared.Domain.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class PostWithSameTitleAlreadyExistsException : AuthorException
{
    public PostWithSameTitleAlreadyExistsException(string title) : base($"Post with title '{title}' already exists")
    {
    }
}