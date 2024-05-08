using AlledrogO.Post.Domain.ValueObjects.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects;

public record PostTag
{
    private string Value { get; }

    private PostTag(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new PostTagCannotBeEmptyException();
        }

        Value = value;
    }

    public static implicit operator string(PostTag tag) => tag.Value;

    public static implicit operator PostTag(string tag) => new(tag);
}