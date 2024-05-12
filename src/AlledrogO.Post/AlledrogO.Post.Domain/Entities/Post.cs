using AlledrogO.Post.Domain.Consts;
using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.Events;
using AlledrogO.Post.Domain.Events.Post;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Entities;

public class Post : AggregateRoot<Guid>
{
    public Guid Id { get; private set; }
    public PostTitle Title { get; private set; }
    public PostDescription Description { get; private set; }
    
    public IEnumerable<PostImage> Images => _images;
    private LinkedList<PostImage> _images = new();
    
    public IEnumerable<Tag> Tags => _tags;
    private LinkedList<Tag> _tags = new();
    
    public PostStatus Status { get; private set; }
    public Author Author { get; private set; }
    public AuthorDetails SharedAuthorDetails { get; private set; }
    
    internal Post(Guid id, PostTitle title, PostDescription description, Author author, AuthorDetails authorDetails)
    {
        Id = id;
        Title = title;
        Description = description;
        Author = author;
        SharedAuthorDetails = authorDetails;
        Status = PostStatus.Draft;
    }
    
    private Post()
    {
    }
    
    internal void Publish()
    {
        Status = PostStatus.Published;
        AddEvent(new PostPublishedDE(this));
    }
    
    internal void Archive()
    {
        Status = PostStatus.Archived;
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
        Description = description;
        AddEvent(new PostDescriptionUpdatedDE(this, Description));
    }
    
    public void UpdateAuthorDetails(AuthorDetails authorDetails)
    {
        SharedAuthorDetails = authorDetails;
        AddEvent(new PostAuthorDetailsUpdatedDE(this, SharedAuthorDetails));
    }
}