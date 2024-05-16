namespace AlledrogO.Post.Application.DTOs;

public class TagDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint PostCount { get; set; }
    public IEnumerable<Guid> PostIds { get; set; }
}