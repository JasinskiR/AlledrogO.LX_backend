namespace AlledrogO.Post.Application.DTOs.External;

public class CreatePostDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public AuthorDetailsDto? AuthorDetails { get; set; }
}