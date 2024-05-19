using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Domain.Entities.Exceptions;

public class TagAlreadyPinnedToPostException : AlledrogoException
{
    public TagAlreadyPinnedToPostException(string tagName, string postTitle) 
        : base($"Tag '{tagName}' is already pinned to post '{postTitle}'")
    {
    }
}