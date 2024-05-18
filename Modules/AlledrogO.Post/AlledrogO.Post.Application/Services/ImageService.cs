using AlledrogO.Post.Domain.ValueObjects;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AlledrogO.Post.Application.Services;

public class ImageService : IImageService
{
    private readonly ImageServiceConfiguration _configuration;
    private readonly IHostEnvironment _environment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageService(IOptions<ImageServiceConfiguration> options,
        IHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
    {
        _environment = environment;
        _httpContextAccessor = httpContextAccessor;
        _configuration = options.Value;
    }



    private class ImageValidator : AbstractValidator<IFormFile>
    {
        private readonly int MaxSize;

        private string extractExtension(string contentType)
        {
            return contentType.Split('/').Last();
        }

        public ImageValidator(int maxSizeMB, string[] allowedFormats)
        {
            MaxSize = maxSizeMB * 1024 * 1024;
            RuleFor(x => x.Length)
                .LessThanOrEqualTo(MaxSize)
                .WithMessage("Image is too large.");
            RuleFor(x => x.ContentType)
                // .Must(x => x == "image/jpeg" || x == "image/png")
                .Must(x => allowedFormats.Contains(extractExtension(x)))
                .WithMessage($"Image must be in one of the following formats: {string.Join(", ", allowedFormats)}");
        }

    }

    public async Task<ValidationResult> ValidateImageAsync(IFormFile image)
    {
        ImageValidator imageValidator = new(_configuration.MaxSizeMB,
            _configuration.AllowedFormats);
        return await imageValidator.ValidateAsync(image);
    }

    /// <summary>
    /// Saves image to the server and returns its path.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<PostImage> SaveImageAsync(IFormFile file)
    {
        var imageId = Guid.NewGuid();
        var fileName = $"{imageId}{Path.GetExtension(file.FileName)}";
        var absPath = Path.Combine(_environment.ContentRootPath, _configuration.StaticFilesDir,
            _configuration.ImageDir, fileName);

        using var stream = new FileStream(absPath, FileMode.Create);
        await file.CopyToAsync(stream);

        // /images/fileName.jpg
        var serverImagePath = string.Join("/", _configuration.ImageDir, fileName);

        var request = _httpContextAccessor.HttpContext.Request;
        var host = request.Host.ToUriComponent();
        var scheme = request.Scheme;
        var fullUrl = $"{scheme}://{host}/{serverImagePath}";

        return new PostImage(imageId, fullUrl);
    }
}