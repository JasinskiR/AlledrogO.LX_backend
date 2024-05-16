using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AlledrogO.Post.Application.Services;

public class ImageValidator : AbstractValidator<IFormFile>
{
    private const int MaxSize = 5 * 1024 * 1024; // 5MB
    public ImageValidator()
    {
        RuleFor(x => x.Length)
            .LessThanOrEqualTo(MaxSize)
            .WithMessage("Image is too large.");
        RuleFor(x => x.ContentType)
            .Must(x => x == "image/jpeg" || x == "image/png")
            .WithMessage("Image must be in jpeg or png format.");
    }
    
}