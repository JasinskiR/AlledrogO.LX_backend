using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Application.Services;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class AddPostImageHandler : ICommandHandler<AddPostImage>
{
    private readonly IPostRepository _postRepository;

    public AddPostImageHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task HandleAsync(AddPostImage command)
    {
        var (postId, file) = command;
        
        ImageValidator imageValidator = new();
        var validationResult = await imageValidator.ValidateAsync(file);
        if (!validationResult.IsValid)
        {
            throw new InvalidImageException(validationResult.Errors);
        }
        
        var post = await _postRepository.GetAsync(postId);
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
        
        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);
        
        var image = new PostImage(path);
        post.AddImage(image);
        await _postRepository.UpdateAsync(post);
    }
}