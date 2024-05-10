using AlledrogO.Post.Application.Commands;
using AlledrogO.Post.Application.Commands.Handlers;
using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;
using NSubstitute;
using Shouldly;

namespace AlledrogO.UnitTests.Application.CommandHandlers;

public class CreatePostWithCustomDetailsHandlerTests
{
    private readonly ICommandHandler<CreatePostWithCustomDetails> _commandHandler;
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPostFactory _postFactory;
    private readonly IAuthorFactory _authorFactory;

    public CreatePostWithCustomDetailsHandlerTests()
    {
        _postRepository = Substitute.For<IPostRepository>();
        _authorRepository = Substitute.For<IAuthorRepository>();
        _postFactory = new PostFactory();
        _authorFactory = new AuthorFactory();
        _commandHandler = new CreatePostWithCustomDetailsHandler(_postRepository, _authorRepository, _postFactory);
    }
    
    Task Act(CreatePostWithCustomDetails command) => _commandHandler.HandleAsync(command);

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
        var command = new CreatePostWithCustomDetails(
            "Post Title", 
            "Post Description", 
            Guid.NewGuid(), 
            CreateAuthorDetailsDto());
        
        _authorRepository.GetAsync(command.AuthorId).Returns(default(Author));
        
        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<AuthorNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_When_Post_Title_Already_Exists_Throws_PostWithSameTitleAlreadyExistsException()
    {
        // Arrange
        var command = new CreatePostWithCustomDetails(
            "Post Title", 
            "Post Description", 
            new Guid(), 
            CreateAuthorDetailsDto());

        var author = CreateAuthor(new Guid());
        _authorRepository.GetAsync(Arg.Any<Guid>()).Returns(author);
        author.AddPost(_postFactory.Create(new Guid(), "Post Title", "Post Description", author));
        
        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PostWithSameTitleAlreadyExistsException>();
    }
    
    [Fact]
    public async Task HandleAsync_When_Author_Is_Not_Null_Creates_Post_Successfully()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var command = new CreatePostWithCustomDetails(
            "Post Title", 
            "Post Description", 
            authorId, 
            CreateAuthorDetailsDto());

        var author = CreateAuthor(authorId);
        _authorRepository.GetAsync(authorId).Returns(author);
        
        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        // Assert
        exception.ShouldBeNull();
    }
}