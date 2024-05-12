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
            Images = model.Images?
                .Select(i => i.Url)
                .ToList() ?? new List<string>(),
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
    
    public static PostCardDto AsCardDto(this PostDbModel model)
    {
        return new PostCardDto
        {
            Id = model.Id,
            Title = model.Title,
            Image = model.Images.FirstOrDefault()?.Url ?? string.Empty,
        };
    }
}