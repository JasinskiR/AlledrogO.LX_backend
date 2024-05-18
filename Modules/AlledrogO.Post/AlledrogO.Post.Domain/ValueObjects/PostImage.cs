using AlledrogO.Post.Domain.ValueObjects.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects;

public record PostImage
{
    public Guid Id { get; }
    public string Url { get; }
    public bool IsMain { get; }

    public PostImage(Guid id, string url, bool isMain = false)
    {
        if (id == Guid.Empty)
        {
            throw new PostImageIdCannotBeEmptyException();
        }
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new PostImageUrlCannotBeEmptyException();
        }

        Id = id;
        Url = url;
        IsMain = isMain;
    }

    public static implicit operator string(PostImage image) => image.Url;
    
    public static implicit operator PostImage(string image) => new(image);
}