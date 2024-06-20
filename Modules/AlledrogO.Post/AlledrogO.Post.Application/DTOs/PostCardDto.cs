using AlledrogO.Post.Domain.Consts;

namespace AlledrogO.Post.Application.DTOs;

public class PostCardDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public PostStatus Status { get; set; }
}