using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.Events;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Entities;

public class Author : AggregateRoot<Guid>
{
    private LinkedList<Post> _posts;
    private AuthorData _authorData;
    
    internal Author(AuthorData authorData, IEnumerable<Post> posts)
    {
        _authorData = authorData;
        _posts = new LinkedList<Post>(posts);
    }
    
    public void AddPost(Post post)
    {
        var nameAlreadyExists = _posts.Any(p => p.Title == post.Title);
        if (nameAlreadyExists)
        {
            throw new PostWithSameTitleAlreadyExistsException(post.Title);
        }
        _posts.AddLast(post);
        AddEvent(new PostAddedDE(this, post));
    }
    
    public void PublishPost(string title)
    {
        var post = getPostByTitle(title);
        post.Publish();
    }
    
    public void ArchivePost(string title)
    {
        var post = getPostByTitle(title);
        post.Archive();
    }
    
    private Post getPostByTitle(string title)
    {
        var post = _posts.FirstOrDefault(p => p.Title == title);
        if (post is null)
        {
            throw new PostNotFoundException(title);
        }
        return post;
    }

    
    
}