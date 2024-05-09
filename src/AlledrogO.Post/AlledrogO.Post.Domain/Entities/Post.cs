using AlledrogO.Post.Domain.Consts;
using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.Events;
using AlledrogO.Post.Domain.Events.Post;
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
    
    internal Post(Guid id, PostTitle title, PostDescription description, Author author, AuthorDetails authorDetails)
    {
        Id = id;
        Title = title;
        _description = description;
        _author = author;
        _sharedAuthorDetails = authorDetails;
        _status = PostStatus.Draft;
    }
    
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
        var imageExists = _images.Contains(image);
        if (imageExists)
        {
            throw new PostImageAlreadyExistsException(image);
        }
        _images.AddLast(image);
        AddEvent(new PostImageAddedDE(this, image));
    }
    
    public void RemoveImage(PostImage image)
    {
        var imageExists = _images.Contains(image);
        if (!imageExists)
        {
            throw new PostImageNotFoundException(image);
        }
        _images.Remove(image);
        AddEvent(new PostImageRemovedDE(this, image));
    }
    
    public void AddTag(Tag tag)
    {
        var tagExists = _tags.Contains(tag);
        if (tagExists)
        {
            throw new TagAlreadyPinnedToPostException(tag.Name, Title);
        }
        _tags.AddLast(tag);
        AddEvent(new TagAddedDE(this, tag));
    }
    
    public void RemoveTag(Tag tag)
    {
        var tagExists = _tags.Contains(tag);
        if (!tagExists)
        {
            throw new TagNotPinnedToPostException(tag.Name, Title);
        }
        _tags.Remove(tag);
        AddEvent(new TagRemovedDE(this, tag));
    }
    
    public void UpdateTitle(PostTitle title)
    {
        Title = title;
        AddEvent(new PostTitleUpdatedDE(this, Title));
    }
    
    public void UpdateDescription(PostDescription description)
    {
        _description = description;
        AddEvent(new PostDescriptionUpdatedDE(this, _description));
    }
    
    public void UpdateAuthorDetails(AuthorDetails authorDetails)
    {
        _sharedAuthorDetails = authorDetails;
        AddEvent(new PostAuthorDetailsUpdatedDE(this, _sharedAuthorDetails));
    }
}