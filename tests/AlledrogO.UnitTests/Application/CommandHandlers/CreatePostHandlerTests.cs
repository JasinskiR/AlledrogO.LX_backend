using AlledrogO.Post.Application.Commands;
using AlledrogO.Post.Application.Commands.Handlers;
using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Post.Domain.ValueObjects.Exceptions;
using AlledrogO.Shared.Commands;
using NSubstitute;
using Shouldly;

namespace AlledrogO.UnitTests.Application.CommandHandlers;

public class CreatePostHandlerTests
{
    private readonly ICommandHandler<CreatePost, Guid> _commandHandler;
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPostFactory _postFactory;
    private readonly IAuthorFactory _authorFactory;

    public CreatePostHandlerTests()
    {
        _postRepository = Substitute.For<IPostRepository>();
        _authorRepository = Substitute.For<IAuthorRepository>();
        _postFactory = new PostFactory();
        _authorFactory = new AuthorFactory();
        _commandHandler = new CreatePostHandler(_postRepository, _authorRepository, _postFactory);
    }
    
    Task<Guid> Act(CreatePost command) => _commandHandler.HandleAsync(command);

    private AuthorDetailsDto CreateAuthorDetailsDto()
        => new AuthorDetailsDto() { Email = "test@mail.com", PhoneNumber = "123456789" };
    private AuthorDetails CreateAuthorDetails()
        => new AuthorDetails("test@mail.com", "123456789");
    
    private Author CreateAuthor(Guid id)
        => _authorFactory.Create(
            id, 
            CreateAuthorDetails(), 
            Enumerable.Empty<Post.Domain.Entities.Post>());
    
    [Fact]
    public async Task HandleAsync_When_Author_Is_Null_Throws_AuthorNotFoundException()
    {
        // Arrange
        var command = new CreatePost(
            "Post Title", 
            "Post Description", 
            Guid.NewGuid(),
            null);
        
        _authorRepository.GetAsync(command.AuthorId).Returns(default(Author));
        
        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<AuthorNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_When_Author_Is_Not_Null_Creates_Post()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var author = CreateAuthor(authorId);
        var command = new CreatePost(
            "Post Title", 
            "Post Description", 
            authorId,
            null);
        
        _authorRepository.GetAsync(authorId).Returns(author);
        
        // Act
        var result = await Act(command);
        
        // Assert
        result.ShouldNotBe(Guid.Empty);
    }
    
    [Fact]
    public async Task HandleAsync_When_AuthorDetailsDto_Is_Not_Null_Creates_Post_With_Custom_Details()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var author = CreateAuthor(authorId);
        var authorDetailsDto = CreateAuthorDetailsDto();
        var command = new CreatePost(
            "Post Title", 
            "Post Description", 
            authorId,
            authorDetailsDto);
        
        _authorRepository.GetAsync(authorId).Returns(author);
        
        // Act
        var result = await Act(command);
        
        // Assert
        result.ShouldNotBe(Guid.Empty);
        author.Posts.ShouldNotBeEmpty();
        author.Posts.First().SharedAuthorDetails.ShouldNotBeNull();
        author.Posts.First().SharedAuthorDetails.Email.ShouldBe(authorDetailsDto.Email);
        author.Posts.First().SharedAuthorDetails.PhoneNumber.ShouldBe(authorDetailsDto.PhoneNumber);
    }
    
    [Fact]
    public async Task HandleAsync_When_AuthorDetailsDto_Is_Invalid_Throws_DtoValidationFailedException()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var author = CreateAuthor(authorId);
        var authorDetailsDto = new AuthorDetailsDto()
        {
            Email = "invalid email",
            PhoneNumber = "123456789"
        };
        var command = new CreatePost(
            "Post Title", 
            "Post Description", 
            authorId,
            authorDetailsDto);
        
        _authorRepository.GetAsync(authorId).Returns(author);
        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<AuthorDataInvalidEmailException>();
    }

}