namespace AlledrogO.Post.Application.DTOs;

public class TagDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint PostCount { get; set; }
    public IEnumerable<Guid> PostIds { get; set; }
}