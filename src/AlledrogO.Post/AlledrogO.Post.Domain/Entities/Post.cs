using AlledrogO.Post.Domain.Consts;
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
    private AuthorData _sharedAuthorData;
    
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
}