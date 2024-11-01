using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Post.Domain.ValueObjects.Exceptions;
using Shouldly;

namespace AlledrogO.UnitTests.Domain;

public class AuthorTests
{
    private readonly IPostFactory _postFactory;
    private readonly IAuthorFactory _authorFactory;
    private readonly IPostImageFactory _postImageFactory;
    
    public AuthorTests()
    {
        _postImageFactory = new PostImageFactory();
        _authorFactory = new AuthorFactory();
        _postFactory = new PostFactory();
    }
    
    private PostImage CreatePostImage(Post.Domain.Entities.Post post)
    {
        return _postImageFactory.Create(Guid.NewGuid(), post, "image1.jpg");
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
    
    [Fact]
    public void PhoneNumber_With9Digits_ShouldCreateValueObjectSuccessfully()
    {
        // Arrange
        var phoneNumber = "123456789";
        var email = "mymail@email.com";
        
        // Act
        var exception = Record.Exception(() => new AuthorDetails(email, phoneNumber));
        
        // Assert
        exception.ShouldBeNull();
    }

    [Theory]
    [InlineData("+1999999999")]
    [InlineData("+12999999999")]
    [InlineData("+123999999999")]
    public void PhoneNumber_WithPrefix_ShouldCreateValueObjectSuccessfully(string phoneNumber)
    {
        // Arrange
        var email = "mymail@email.com";
        
        // Act
        var exception = Record.Exception(() => new AuthorDetails(email, phoneNumber));
        
        // Assert
        exception.ShouldBeNull();
    }
    
    [Theory]
    [InlineData("12345678")]
    [InlineData("1234567890")]
    [InlineData("-48123456789")]
    public void PhoneNumber_WithInvalidLength_ShouldThrowException(string phoneNumber)
    {
        // Arrange
        var email = "mymail@email.com";
        
        // Act
        var exception = Record.Exception(() => new AuthorDetails(email, phoneNumber));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<AuthorDataInvalidPhoneNumberException>();
    }
    
    
    [Fact]
    public void AddPost_With_New_Post_Should_Add_Post_Successfully()
    {
        // Arrange
        var author = CreateAuthor();
        var post = CreatePost(author);
        
        // Act
        var exception = Record.Exception(() => author.AddPost(post));
        
        // Assert
        exception.ShouldBeNull();
        author.Posts.ShouldContain(post);
    }
    
    [Fact]
    public void AddPost_With_Post_With_Same_Title_Should_Throw_Exception()
    {
        // Arrange
        var author = CreateAuthor();
        var post1 = CreatePost(author, "Title 1", "Description 1");
        var post2 = CreatePost(author, "Title 1", "Description 2");
        author.AddPost(post1);
        
        // Act
        var exception = Record.Exception(() => author.AddPost(post2));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PostWithSameTitleAlreadyExistsException>();
    }

}