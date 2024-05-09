namespace AlledrogO.Post.Infrastructure.EF.Models.ReadModels;

public class TagReadDbModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint PostCount { get; set; }
    public ICollection<PostReadDbModel> Posts { get; set; }
}