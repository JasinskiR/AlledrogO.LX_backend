using AlledrogO.Post.Domain.Consts;

namespace AlledrogO.Post.Application.DTOs;

public class PostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<string> Images { get; set; }
    public IEnumerable<string> Tags { get; set; }
    public PostStatus Status { get; set; }
    public Guid AuthorId { get; set; }
    public AuthorDetailsDto AuthorDetails { get; set; }
}