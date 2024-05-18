using AlledrogO.Post.Domain.ValueObjects;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AlledrogO.Post.Application.Services;

public interface IImageService
{
    public Task<ValidationResult> ValidateImageAsync(IFormFile image);
    public Task<PostImage> SaveImageAsync(IFormFile file);
}