using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Entities;

public class Tag : AggregateRoot<Guid>
{
    public TagName Name { get; private set; }
    private List<Post> _posts = new();
    public uint PostCount { get; private set; }
    
    internal Tag(Guid id, TagName name, IEnumerable<Post> posts)
    {
        Id = id;
        Name = name;
        AddManyPosts(posts);
        PostCount = (uint)_posts.Count;
    }
    
    private Tag()
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
        var postAlreadyExists = _posts.Any(p => p.Id == post.Id);
        if (postAlreadyExists)
        {
            throw new TagAlreadyPinnedToPostException(Name, post.Title);
        }
        _posts.Add(post);
        PostCount++;
    }
    
    public void RemovePost(Post post)
    {
        var postToRemove = _posts.FirstOrDefault(p => p.Id == post.Id);
        if (postToRemove is null)
        {
            throw new TagNotPinnedToPostException(Name, post.Title);
        }
        _posts.Remove(postToRemove);
        PostCount--;
    }
    
}