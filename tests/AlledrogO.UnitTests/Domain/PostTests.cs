using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.Events.Post;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using Shouldly;

namespace AlledrogO.UnitTests.Domain;

public class PostTests
{
    private readonly IPostFactory _postFactory;
    private readonly IAuthorFactory _authorFactory;
    public PostTests()
    {
        _authorFactory = new AuthorFactory();
        _postFactory = new PostFactory();
    }
    
    private Post.Domain.Entities.Post CreatePost()
    {
        var authorDetails = new AuthorDetails("author@mail.com", "123456789");
        var author = _authorFactory.Create(
            new Guid(), 
            authorDetails, 
            Enumerable.Empty<Post.Domain.Entities.Post>());
        return _postFactory.Create(
            new Guid(),
            "Post 1",
            "Description 1",
            author);
    }
    
    [Fact]
    public void AuthorDetails_With_Valid_Email_And_Phone_Should_Create_ValueObject_Successfully()
    {
        // Arrange
        var email = "author@mail.com";
        var phone = "123456789";
        
        // Act
        var exception = Record.Exception(() => new AuthorDetails(email, phone));
        
        // Assert
        exception.ShouldBeNull();
    }
    
    [Fact]
    public void AddImage_With_New_Image_Should_Add_Image_Successfully()
    {
        // Arrange
        var post = CreatePost();
    
        // Act
        var exception = Record.Exception(() => post.AddImage(new PostImage("image1.jpg")));
    
        // Assert
        exception.ShouldBeNull();
        post.Events.Count().ShouldBe(1);
        post.Events.First().ShouldBeOfType<PostImageAddedDE>();
    }
    
    [Fact]
    public void AddImage_With_Existing_Image_Should_Throw_PostImageAlreadyExistsException()
    {
        // Arrange
        var post = CreatePost();
        post.AddImage(new PostImage("image1.jpg"));
        
        // Act
        var exception = Record.Exception(() => post.AddImage(new PostImage("image1.jpg")));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PostImageAlreadyExistsException>();
    }
    
    [Fact]
    public void RemoveImage_With_Existing_Image_Should_Remove_Image_Successfully()
    {
        // Arrange
        var post = CreatePost();
        post.AddImage(new PostImage("image1.jpg"));
        
        // Act
        var exception = Record.Exception(() => post.RemoveImage(new PostImage("image1.jpg")));
        
        // Assert
        exception.ShouldBeNull();
        post.Events.Count().ShouldBe(2);
        post.Events.Last().ShouldBeOfType<PostImageRemovedDE>();
    }
    
    [Fact]
    public void RemoveImage_With_Non_Existing_Image_Should_Throw_PostImageNotFoundException()
    {
        // Arrange
        var post = CreatePost();
        
        // Act
        var exception = Record.Exception(() => post.RemoveImage(new PostImage("image1.jpg")));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PostImageNotFoundException>();
    }
}