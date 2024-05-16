using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class TagNotPinnedToPostException : AlledrogoException
{
    public TagNotPinnedToPostException(string name, string postTitle) 
        : base($"Tag '{name}' is not pinned to post '{postTitle}'")
    {
    }
}