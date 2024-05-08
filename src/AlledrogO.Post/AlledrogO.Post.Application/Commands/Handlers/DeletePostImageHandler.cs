using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.Repositories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class DeletePostImageHandler : ICommandHandler<DeletePostImage>
{
    private readonly IPostRepository _postRepository;

    public DeletePostImageHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task HandleAsync(DeletePostImage command)
    {
        var (postId, imageUrl) = command;
        var post = await _postRepository.GetAsync(postId);
        
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        post.RemoveImage(imageUrl);
        await _postRepository.UpdateAsync(post);
    }
}