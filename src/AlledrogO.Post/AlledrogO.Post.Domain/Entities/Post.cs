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
    private LinkedList<Tag> _tags = new();
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
    
    internal Post(Guid id, PostTitle title, PostDescription description, Author author, AuthorDetails authorDetails)
    {
        Id = id;
        Title = title;
        _description = description;
        _author = author;
        _sharedAuthorDetails = authorDetails;
        _status = PostStatus.Draft;
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
    public void AddTag(Tag tag)
    {
        var tagExists = _tags.Any(t => t.Id == tag.Id);
        if (tagExists)
        {
            throw new TagAlreadyPinnedToPostException(tag.Name, Title);
        }
        _tags.AddLast(tag);
    }
    public void RemoveTag(Tag tag)
    {
        var tagExists = _tags.Contains(tag);
        if (!tagExists)
        {
            throw new TagNotPinnedToPostException(tag.Name, Title);
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