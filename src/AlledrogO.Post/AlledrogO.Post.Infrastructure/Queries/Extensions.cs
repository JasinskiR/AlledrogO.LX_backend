using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Infrastructure.EF.Models;

namespace AlledrogO.Post.Infrastructure.Queries;

public static class Extensions
{
    public static PostDto AsDto(this PostDbModel model)
    {
        return new PostDto
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Images = model.Images
                .Select(i => i.Url)
                .ToList(),
            Tags = model.Tags
                .Select(t => t.Name)
                .ToList(),
            Status = model.Status,
            AuthorId = model.Author.Id,
            AuthorDetails = model.AuthorDetails.AsDto()
        };
    }
    
    public static AuthorDetailsDto AsDto(this AuthorDetailsReadDbModel model)
    {
        return new AuthorDetailsDto
        {
            Email = model.Email,
            PhoneNumber = model.PhoneNumber
        };
    }
}