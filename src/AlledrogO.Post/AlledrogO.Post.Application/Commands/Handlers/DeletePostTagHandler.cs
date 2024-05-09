using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class DeletePostTagHandler : ICommandHandler<DeletePostTag>
{
    private readonly IPostRepository _postRepository;

    public DeletePostTagHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task HandleAsync(DeletePostTag command)
    {
        var (postId, tag) = command;
        var post = await _postRepository.GetAsync(postId);
        
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        post.RemoveTag(tag);
        await _postRepository.UpdateAsync(post);
    }
}