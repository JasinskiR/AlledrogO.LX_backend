using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;
using FluentValidation;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class UpdatePostHandler : ICommandHandler<UpdatePost>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPostFactory _postFactory;

    public UpdatePostHandler(IPostRepository postRepository, IAuthorRepository authorRepository, IPostFactory postFactory)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
        _postFactory = postFactory;
    }

    public async Task HandleAsync(UpdatePost command)
    {
        var (id, title, description, authorDetailsDto) = command;
        var validator = new UpdatePostValidator();
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            throw new DtoValidationFailedException(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
        }
        
        var post = await _postRepository.GetAsync(id);
        if (post is null)
        {
            throw new PostNotFoundException(id);
        }
        
        var authorDetails = default(AuthorDetails);
        
        if (authorDetailsDto is not null)
        { 
            authorDetails= new AuthorDetails(authorDetailsDto.Email, authorDetailsDto.PhoneNumber);
            post.UpdateAuthorDetails(authorDetails);
        }
        post.UpdateTitle(title);
        post.UpdateDescription(description);
        await _postRepository.UpdateAsync(post);
    }
    
    private class UpdatePostValidator : AbstractValidator<UpdatePost>
    {
        public UpdatePostValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.PostId).NotEmpty();
        }
    }
}