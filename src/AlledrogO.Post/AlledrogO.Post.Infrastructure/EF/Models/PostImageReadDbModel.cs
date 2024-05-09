namespace AlledrogO.Post.Infrastructure.EF.Models;

public class PostImageReadDbModel
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public PostReadDbModel Post { get; set; }
}