using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Infrastructure.EF.Models;

namespace AlledrogO.Post.Infrastructure.Queries;

public static class Extensions
{
    internal static PostDto AsDto(this PostDbModel model)
    {
        return new PostDto
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Images = model.Images?
                .Select(i => i.AsDto()
                ).ToList() ?? new List<PostImageDto>(),
            Tags = model.Tags
                .Select(t => t.Name)
                .ToList(),
            Status = model.Status,
            AuthorId = model.Author.Id,
            AuthorDetails = new AuthorDetailsDto()
                {
                    Email = model.SharedAuthorDetails.Split(", ")[0],
                    PhoneNumber = model.SharedAuthorDetails.Split(", ")[1]
                }

        };
    }
    
    internal static PostCardDto AsCardDto(this PostDbModel model)
    {
        return new PostCardDto
        {
            Id = model.Id,
            Title = model.Title,
            Image = model.Images
                .FirstOrDefault(i => i.IsMain)?
                .Url ?? string.Empty,
            Status = model.Status
        };
    }
    
    internal static AuthorDto AsDto(this AuthorDbModel model)
    {
        return new AuthorDto
        {
            Id = model.Id,
            Details = model.AuthorDetails.AsDto(),
            Posts = model.Posts?
                .Select(p => p.Id)
                .ToList() ?? new List<Guid>()
        };
    }
    
    private static AuthorDetailsDto AsDto(this string details)
    {
        return new AuthorDetailsDto
        {
            Email = details.Split(", ")[0],
            PhoneNumber = details.Split(", ")[1]
        };
    }
    
    internal static TagDetailsDto AsDetailsDto(this TagDbModel model)
    {
        return new TagDetailsDto
        {
            Id = model.Id,
            Name = model.Name,
            PostCount = model.PostCount,
            PostIds = model.Posts?
                .Select(p => p.Id)
                .ToList() ?? new List<Guid>()
        };
    }
    
    internal static TagDto AsDto(this TagDbModel model)
    {
        return new TagDto
        {
            Name = model.Name,
            PostCount = model.PostCount,
        };
    }
    
    internal static PostImageDto AsDto(this PostImageDbModel model)
    {
        return new PostImageDto
        {
            Id = model.Id,
            Url = model.Url,
            IsMain = model.IsMain
        };
    }
}