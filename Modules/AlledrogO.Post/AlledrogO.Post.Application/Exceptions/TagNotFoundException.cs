using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Post.Application.Exceptions;

public class TagNotFoundException : AlledrogoException
{
    public TagNotFoundException(string name) : base($"Tag with name '{name}' not found")
    {
    }
}