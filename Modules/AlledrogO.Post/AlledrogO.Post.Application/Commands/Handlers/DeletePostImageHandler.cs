using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Services;
using AlledrogO.Post.Domain.Entities.Exceptions;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;
using PostNotFoundException = AlledrogO.Post.Application.Exceptions.PostNotFoundException;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class DeletePostImageHandler : ICommandHandler<DeletePostImage>
{
    private readonly IPostRepository _postRepository;
    private readonly IImageService _imageService;

    public DeletePostImageHandler(IPostRepository postRepository, IImageService imageService)
    {
        _postRepository = postRepository;
        _imageService = imageService;
    }

    public async Task HandleAsync(DeletePostImage command)
    {
        var (postId, imageId) = command;
        var post = await _postRepository.GetAsync(postId);
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        
        var image = post.Images.FirstOrDefault(i => i.Id == imageId);
        if (image is null)
        {
            throw new PostImageNotFoundException(imageId);
        }
        
        post.RemoveImage(image);
        await _postRepository.UpdateAsync(post);
        await _imageService.DeleteImageAsync(image.Url);
    }
}