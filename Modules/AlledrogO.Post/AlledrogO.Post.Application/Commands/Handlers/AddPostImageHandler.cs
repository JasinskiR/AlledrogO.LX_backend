using System.Security.Claims;
using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Application.Services;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class AddPostImageHandler : ICommandHandler<AddPostImage, string>
{
    private readonly IPostRepository _postRepository;
    private readonly IImageService _imageService;
    private readonly IPostImageFactory _postImageFactory;
    
    public AddPostImageHandler(IPostRepository postRepository, 
        IImageService imageService, 
        IPostImageFactory postImageFactory)
    {
        _postRepository = postRepository;
        _imageService = imageService;
        _postImageFactory = postImageFactory;
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
        
        var imageId = Guid.NewGuid();
        var imageUrl = await _imageService.SaveImageAsync(file, imageId);
        var savedImage = _postImageFactory.Create(imageId, post, imageUrl);
        
        
        post.AddImage(savedImage);
        await _postRepository.UpdateAsync(post);
        return imageUrl;
    }
}