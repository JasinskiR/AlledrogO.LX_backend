using AlledrogO.Post.Domain.ValueObjects.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects;

public record TagName
{
    private string Value { get; }

    public TagName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new TagNameCannotBeEmptyException();
        }

        Value = value;
    }

    public static implicit operator string(TagName tag) => tag.Value;

    public static implicit operator TagName(string tag) => new(tag);
}