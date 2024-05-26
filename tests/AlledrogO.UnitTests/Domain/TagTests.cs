using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using Shouldly;

namespace AlledrogO.UnitTests.Domain;

public class TagTests
{
    private readonly IPostFactory _postFactory;
    private readonly IAuthorFactory _authorFactory;
    private readonly IPostImageFactory _postImageFactory;
    private readonly ITagFactory _tagFactory;
    
    public TagTests()
    {
        _postImageFactory = new PostImageFactory();
        _authorFactory = new AuthorFactory();
        _postFactory = new PostFactory();
        _tagFactory = new TagFactory();
    }
    
    private Author CreateAuthor()
    {
        var authorDetails = new AuthorDetails("author@mail.com", "123456789");
        var author = _authorFactory.Create(
            new Guid(), 
            authorDetails, 
            Enumerable.Empty<Post.Domain.Entities.Post>());
        return author;
    }
    
    private Post.Domain.Entities.Post CreatePost(Author author, 
        string title = "Post 1", 
        string description = "Description 1")
    {
        var post = _postFactory.Create(
            Guid.NewGuid(),
            title,
            description,
            author);
        return post;
    }
    
    private Tag CreateTag(string name = "Tag 1")
    {
        var tag = _tagFactory.Create(
            Guid.NewGuid(),
            new TagName(name),
            Enumerable.Empty<Post.Domain.Entities.Post>());
        return tag;
    }
    
    [Fact]
    public void AddPost_With_New_Post_Should_Add_Post_Successfully()
    {
        // Arrange
        var author = CreateAuthor();
        var post = CreatePost(author);
        var tag = CreateTag();
        
        // Act
        var exception = Record.Exception(() => tag.AddPost(post));
        
        // Assert
        exception.ShouldBeNull();
    }
    
    [Fact]
    public void AddPost_With_Same_Post_Should_Throw_Exception()
    {
        // Arrange
        var author = CreateAuthor();
        var post = CreatePost(author);
        var tag = CreateTag();
        tag.AddPost(post);
        
        // Act
        var exception = Record.Exception(() => tag.AddPost(post));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<TagAlreadyPinnedToPostException>();
    }
    
    [Fact]
    public void RemovePost_With_Existing_Post_Should_Remove_Post_Successfully()
    {
        // Arrange
        var author = CreateAuthor();
        var post = CreatePost(author);
        var tag = CreateTag();
        tag.AddPost(post);
        
        // Act
        var exception = Record.Exception(() => tag.RemovePost(post));
        
        // Assert
        exception.ShouldBeNull();
    }
    
    [Fact]
    public void RemovePost_With_Non_Existing_Post_Should_Throw_Exception()
    {
        // Arrange
        var author = CreateAuthor();
        var post = CreatePost(author);
        var tag = CreateTag();
        
        // Act
        var exception = Record.Exception(() => tag.RemovePost(post));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<TagNotPinnedToPostException>();
    }
}