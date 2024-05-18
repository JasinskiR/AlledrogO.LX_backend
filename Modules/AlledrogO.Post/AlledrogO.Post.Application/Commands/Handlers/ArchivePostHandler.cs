using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class ArchivePostHandler : ICommandHandler<ArchivePost>
{
    private readonly IPostRepository _postRepository;

    public ArchivePostHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task HandleAsync(ArchivePost command)
    {
        var postId = command.PostId;
        var post = await _postRepository.GetAsync(postId);
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        post.Archive();
        await _postRepository.UpdateAsync(post);
    }
}