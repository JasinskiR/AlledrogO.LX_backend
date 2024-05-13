namespace AlledrogO.Post.Application.DTOs;

public class AuthorDto
{
    public Guid Id { get; set; }
    public AuthorDetailsDto Details { get; set; }
    public IEnumerable<Guid> Posts { get; set; }
}