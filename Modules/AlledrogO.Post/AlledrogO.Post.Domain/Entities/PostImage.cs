using AlledrogO.Post.Domain.ValueObjects.Exceptions;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Entities;

public class PostImage : Entity<Guid>
{
    public Guid Id { get; private set; }
    
    public string Url { get; private set; }
    public bool IsMain { get; private set; }
    public Post Post { get; private set; }
    
    private PostImage()
    {
    }
    
    internal PostImage(Guid id, Post post, string url, bool isMain = false)
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
    
    public void SetAsMain()
    {
        IsMain = true;
    }
    
    public void SetAsNotMain()
    {
        IsMain = false;
    }
    
}