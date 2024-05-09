using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class AddPostTagHandler : ICommandHandler<AddPostTag>
{
    private readonly IPostRepository _postRepository;

    public AddPostTagHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task HandleAsync(AddPostTag command)
    {
        var (postId, tag) = command;
        var post = await _postRepository.GetAsync(postId);
        
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        post.AddTag(tag);
        await _postRepository.UpdateAsync(post);
    }
}