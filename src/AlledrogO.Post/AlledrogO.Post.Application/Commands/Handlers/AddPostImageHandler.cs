using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Application.Services;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;
using Microsoft.Extensions.Hosting;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class AddPostImageHandler : ICommandHandler<AddPostImage, string>
{
    private readonly IPostRepository _postRepository;
    private readonly IHostEnvironment _environment;
    private static string _imagesDirectory = "images";
    private readonly string _staticFilesPath;

    public AddPostImageHandler(IPostRepository postRepository, IHostEnvironment environment)
    {
        _postRepository = postRepository;
        _environment = environment;
        _staticFilesPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
    }

    public async Task<string> HandleAsync(AddPostImage command)
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
        var absPath = Path.Combine(_staticFilesPath, _imagesDirectory, fileName);
        
        using var stream = new FileStream(absPath, FileMode.Create);
        await file.CopyToAsync(stream);

        var serverImagePath = string.Join("/", _imagesDirectory, fileName);
        var image = new PostImage(serverImagePath);
        post.AddImage(image);
        await _postRepository.UpdateAsync(post);
        return serverImagePath;
    }
}