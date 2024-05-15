using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Shared.Commands;
using FluentValidation;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class CreatePostHandler : ICommandHandler<CreatePost, Guid>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPostFactory _postFactory;

    public CreatePostHandler(IPostRepository postRepository, IAuthorRepository authorRepository, IPostFactory postFactory)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
        _postFactory = postFactory;
    }

    public async Task<Guid> HandleAsync(CreatePost command)
    {
        var ( title, description, authorId) = command;
        var validator = new CreatePostValidator();
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            throw new DtoValidationFailedException(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
        }
        
        var author = await _authorRepository.GetAsync(authorId);
        
        if (author is null)
        {
            throw new AuthorNotFoundException(authorId);
        }
        var id = Guid.NewGuid();
        var post = _postFactory.Create(id, title, description, author);
        author.AddPost(post);
        await _postRepository.AddAsync(post);
        await _authorRepository.UpdateAsync(author);

        return id;
    }
    
    private class CreatePostValidator : AbstractValidator<CreatePost>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.AuthorId).NotEmpty();
        }
    }
}