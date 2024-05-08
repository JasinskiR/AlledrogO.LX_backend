using AlledrogO.Post.Domain.ValueObjects.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects;

public record PostDescription
{
    private string Value { get; }

    private PostDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new PostDescriptionCannotBeEmptyException();
        }

        Value = value;
    }

    public static implicit operator string(PostDescription description) => description.Value;

    public static implicit operator PostDescription(string description) => new(description);
    
}