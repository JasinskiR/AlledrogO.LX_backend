using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.Repositories;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class UpdatePostDescriptionHandler : ICommandHandler<UpdatePostDescription>
{
    private readonly IPostRepository _postRepository;

    public UpdatePostDescriptionHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task HandleAsync(UpdatePostDescription command)
    {
        var (postId, description) = command;
        var post = await _postRepository.GetAsync(postId);
        
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        
        post.UpdateDescription(description);
        await _postRepository.UpdateAsync(post);
    }
}