using AlledrogO.Post.Domain.Entities;
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
    private readonly IPostImageFactory _postImageFactory;
    
    public PostTests()
    {
        _postImageFactory = new PostImageFactory();
        _authorFactory = new AuthorFactory();
        _postFactory = new PostFactory();
    }
    
    private PostImage CreatePostImage(Post.Domain.Entities.Post post)
    {
        return _postImageFactory.Create(Guid.NewGuid(), post, "image1.jpg");
    }
    
    private Post.Domain.Entities.Post CreatePost()
    {
        var authorDetails = new AuthorDetails("author@mail.com", "123456789");
        var author = _authorFactory.Create(
            new Guid(), 
            authorDetails, 
            Enumerable.Empty<Post.Domain.Entities.Post>());
        var post = _postFactory.Create(
            new Guid(),
            "Post 1",
            "Description 1",
            author);
        return post;
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
        var image = CreatePostImage(post);
    
        // Act
        var exception = Record.Exception(() => post.AddImage(image));
    
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
        var image = CreatePostImage(post);
        post.AddImage(image);
        
        // Act
        var exception = Record.Exception(() => post.AddImage(image));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PostImageAlreadyExistsException>();
    }
    
    [Fact]
    public void RemoveImage_With_Existing_Image_Should_Remove_Image_Successfully()
    {
        // Arrange
        var post = CreatePost();
        var image = CreatePostImage(post);
        post.AddImage(image);
        
        // Act
        var exception = Record.Exception(() => post.RemoveImage(image));
        
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
        var image = CreatePostImage(post);
        
        // Act
        var exception = Record.Exception(() => post.RemoveImage(image));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PostImageNotFoundException>();
    }
    
    [Fact]
    public void SetImageAsMain_When_2_Images_Are_There_Should_Set_First_Image_As_Main_Successfully()
    {
        // Arrange
        var post = CreatePost();
        var image1 = CreatePostImage(post);
        var image2 = CreatePostImage(post);
        post.AddImage(image1);
        
        // Act
        var exception = Record.Exception(() =>  post.AddImage(image2));
        
        // Assert
        exception.ShouldBeNull();
        image1.IsMain.ShouldBeTrue();
        image2.IsMain.ShouldBeFalse();
    }
    
    [Fact]
    public void SetImageAsMain_Should_Set_Image_As_Main_And_Unset_Others_Successfully()
    {
        // Arrange
        var post = CreatePost();
        var image1 = CreatePostImage(post);
        var image2 = CreatePostImage(post);
        post.AddImage(image1);
        post.AddImage(image2);
        
        // Act
        var exception = Record.Exception(() => post.SetImageAsMain(image2));
        
        // Assert
        exception.ShouldBeNull();
        image1.IsMain.ShouldBeFalse();
        image2.IsMain.ShouldBeTrue();
    }
}