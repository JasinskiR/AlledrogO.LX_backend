using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class UpdatePostAuthorDetailsHandler : ICommandHandler<UpdatePostAuthorDetails>
{
    private readonly IPostRepository _postRepository;

    public UpdatePostAuthorDetailsHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task HandleAsync(UpdatePostAuthorDetails command)
    {
        var (postId, email, phoneNumber) = command;
        var post = await _postRepository.GetAsync(postId);
        
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        
        var authorDetails = new AuthorDetails(email, phoneNumber);
        post.UpdateAuthorDetails(authorDetails);
    }
}