using AlledrogO.Post.Domain.ValueObjects.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects;

public record PostTitle
{
    private string Value { get; }
    private PostTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new PostTitleCannotBeEmptyException();
        }

        Value = value;
    }

    public static implicit operator string(PostTitle title) => title.Value;

    public static implicit operator PostTitle(string title) => new(title);
}