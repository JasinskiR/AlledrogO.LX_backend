using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.Events;
using AlledrogO.Post.Domain.Events.Author;
using AlledrogO.Post.Domain.Events.Post;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Entities;

public class Author : AggregateRoot<Guid>
{
    public Guid Id { get; private set; }
    public IEnumerable<Post> Posts => _posts;
    private LinkedList<Post> _posts = new();
    public AuthorDetails AuthorDetails { get; private set; }
    
    internal Author(Guid id, AuthorDetails authorDetails, IEnumerable<Post> posts)
    {
        Id = id;
        AuthorDetails = authorDetails;
        AddManyPosts(posts);
    }
    
    private Author()
    {
    }
    
    private void AddManyPosts(IEnumerable<Post> posts)
    {
        foreach (var post in posts)
        {
            AddPost(post);
        }
    }
    
    public void AddPost(Post post)
    {
        var nameAlreadyExists = _posts.Any(p => p.Title == post.Title);
        if (nameAlreadyExists)
        {
            throw new PostWithSameTitleAlreadyExistsException(post.Title);
        }
        _posts.AddLast(post);
        AddEvent(new AuthorPostAddedDE(this, post));
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
    
    public void DeleteAllPosts()
    {
        DeleteManyPosts(Posts);
    }
    
    public void DeletePost(Post post)
    {
        if (!Posts.Contains(post))
        {
            throw new PostNotFoundException(post.Title);
        }
        _posts.Remove(post);
        AddEvent(new AuthorPostDeletedDE(this, post));
    }
    
    public void DeleteManyPosts(IEnumerable<Post> posts)
    {
        foreach (var post in posts)
        {
            DeletePost(post);
        }
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