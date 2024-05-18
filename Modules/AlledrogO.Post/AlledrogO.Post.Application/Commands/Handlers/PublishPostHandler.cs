using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class PublishPostHandler : ICommandHandler<PublishPost>
{
    private readonly IPostRepository _postRepository;

    public PublishPostHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task HandleAsync(PublishPost command)
    {
        var postId = command.PostId;
        var post = await _postRepository.GetAsync(postId);
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        post.Publish();
        await _postRepository.UpdateAsync(post);
    }
}