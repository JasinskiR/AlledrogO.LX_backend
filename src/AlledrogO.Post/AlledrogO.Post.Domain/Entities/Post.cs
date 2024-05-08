using AlledrogO.Post.Domain.Consts;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Entities;

public class Post : AggregateRoot<Guid>
{
    private PostTitle _title;
    private PostDescription _description;
    private LinkedList<PostImage> _images = new();
    private List<PostTag> _tags = new();
    private PostStatus _status;
    private Author _author;
    private AuthorData _sharedAuthorData;
}