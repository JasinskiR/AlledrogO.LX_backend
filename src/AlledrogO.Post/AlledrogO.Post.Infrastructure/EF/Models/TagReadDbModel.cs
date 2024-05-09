namespace AlledrogO.Post.Infrastructure.EF.Models;

public class TagReadDbModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint PostCount { get; set; }
    public ICollection<PostReadDbModel> Posts { get; set; }
}