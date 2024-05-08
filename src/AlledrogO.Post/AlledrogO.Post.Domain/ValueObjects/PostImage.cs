using AlledrogO.Post.Domain.ValueObjects.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects;

public record PostImage
{
    private string Url { get; }

    private PostImage(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new PostImageUrlCannotBeEmptyException();
        }

        Url = url;
    }

    public static implicit operator string(PostImage image) => image.Url;
    
    public static implicit operator PostImage(string image) => new(image);
}