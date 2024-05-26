using AlledrogO.Post.Application.Commands;
using AlledrogO.Post.Application.Commands.Handlers;
using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;
using NSubstitute;
using Shouldly;

namespace AlledrogO.UnitTests.Application.CommandHandlers;

public class UpdatePostHandlerTests
{
    private readonly ICommandHandler<UpdatePost> _commandHandler;
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPostFactory _postFactory;
    private readonly IAuthorFactory _authorFactory;

    public UpdatePostHandlerTests()
    {
        _postRepository = Substitute.For<IPostRepository>();
        _authorRepository = Substitute.For<IAuthorRepository>();
        _postFactory = new PostFactory();
        _authorFactory = new AuthorFactory();
        _commandHandler = new UpdatePostHandler(_postRepository, _authorRepository, _postFactory);
    }
    
    Task Act(UpdatePost command) => _commandHandler.HandleAsync(command);
        
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
    public async Task HandleAsync_When_Post_Is_Null_Throws_PostNotFoundException()
    {
        var author = CreateAuthor(Guid.NewGuid());
        var post = _postFactory.Create(
            Guid.NewGuid(),
            "Post Title",
            "Post Description",
            author);
        // Arrange
        var command = new UpdatePost(
            post.Id,
            "Post Title", 
            "Post Description", 
            null);
        
        _authorRepository.GetAsync(author.Id).Returns(author);
        _postRepository.GetAsync(post.Id).Returns(default(Post.Domain.Entities.Post));
        
        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PostNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_When_Post_Is_Not_Null_Updates_Post()
    {
        var author = CreateAuthor(Guid.NewGuid());
        var post = _postFactory.Create(
            Guid.NewGuid(),
            "Post Title",
            "Post Description",
            author);
        // Arrange
        var command = new UpdatePost(
            post.Id,
            "Post Title v2", 
            "Post Description v2", 
            null);
        
        _authorRepository.GetAsync(author.Id).Returns(author);
        _postRepository.GetAsync(post.Id).Returns(post);
        
        // Act
        await Act(command);
        
        // Assert
        post.Title.Value.ShouldBeSameAs("Post Title v2");
        post.Description.Value.ShouldBeSameAs("Post Description v2");
    }
}