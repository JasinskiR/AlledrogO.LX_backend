using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class RenamePostTitleHandler : ICommandHandler<RenamePostTitle>
{
    private readonly IPostRepository _postRepository;

    public RenamePostTitleHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task HandleAsync(RenamePostTitle command)
    {
        var (postId, title) = command;
        var post = await _postRepository.GetAsync(postId);
        
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        post.UpdateTitle(title);
        await _postRepository.UpdateAsync(post);
    }
}