using AlledrogO.Post.Domain.Consts;
using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.Events;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Entities;

public class Post : AggregateRoot<Guid>
{
    public PostTitle Title { get; private set; }
    private PostDescription _description;
    private LinkedList<PostImage> _images = new();
    private List<PostTag> _tags = new();
    private PostStatus _status;
    private Author _author;
    private AuthorDetails _sharedAuthorDetails;
    
    internal void Publish()
    {
        _status = PostStatus.Published;
        AddEvent(new PostPublishedDE(this));
    }
    
    internal void Archive()
    {
        _status = PostStatus.Archived;
        AddEvent(new PostArchivedDE(this));
    }
    
    public void AddImage(PostImage image)
    {
        _images.AddLast(image);
    }
    public void RemoveImage(PostImage image)
    {
        var imageExists = _images.Contains(image);
        if (!imageExists)
        {
            throw new PostImageNotFoundException(image);
        }
        _images.Remove(image);
    }
    public void AddTag(PostTag tag)
    {
        var tagExists = _tags.Contains(tag);
        if (tagExists)
        {
            throw new PostTagAlreadyExistsException(tag);
        }
        _tags.Add(tag);
    }
    public void RemoveTag(PostTag tag)
    {
        var tagExists = _tags.Contains(tag);
        if (!tagExists)
        {
            throw new PostTagNotFoundException(tag);
        }
        _tags.Remove(tag);
    }
    public void UpdateTitle(PostTitle title)
    {
        Title = title;
    }
    public void UpdateDescription(PostDescription description)
    {
        _description = description;
    }
    public void UpdateAuthorDetails(AuthorDetails authorDetails)
    {
        _sharedAuthorDetails = authorDetails;
    }
}