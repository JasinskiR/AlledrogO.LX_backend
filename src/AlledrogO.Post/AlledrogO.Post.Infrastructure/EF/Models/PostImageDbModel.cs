namespace AlledrogO.Post.Infrastructure.EF.Models;

public class PostImageDbModel
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public PostDbModel Post { get; set; }
}