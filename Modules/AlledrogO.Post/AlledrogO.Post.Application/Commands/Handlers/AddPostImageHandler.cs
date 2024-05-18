using System.Security.Claims;
using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Application.Services;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class AddPostImageHandler : ICommandHandler<AddPostImage, string>
{
    private readonly IPostRepository _postRepository;
    private readonly IImageService _imageService;
    
    public AddPostImageHandler(IPostRepository postRepository, IImageService imageService)
    {
        _postRepository = postRepository;
        _imageService = imageService;
    }
    

    public async Task<string> HandleAsync(AddPostImage command)
    {
        var (postId, file) = command;
        var post = await _postRepository.GetAsync(postId);
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        
        var validationResult = await _imageService.ValidateImageAsync(file);
        if (!validationResult.IsValid)
        {
            throw new InvalidImageException(validationResult.Errors);
        }
        
        var image = await _imageService.SaveImageAsync(file);
        
        post.AddImage(image);
        await _postRepository.UpdateAsync(post);
        return image.Url;
    }
}