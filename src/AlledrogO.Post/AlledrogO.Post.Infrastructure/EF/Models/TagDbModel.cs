namespace AlledrogO.Post.Infrastructure.EF.Models;

public class TagDbModel
{
    public Guid Id { get; set; }
    public uint Version { get; set; }
    public string Name { get; set; }
    public uint PostCount { get; set; }
    public ICollection<PostDbModel> Posts { get; set; }
}