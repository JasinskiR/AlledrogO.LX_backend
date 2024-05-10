namespace AlledrogO.Post.Infrastructure.EF.Models.ReadModels;

public class PostImageReadDbModel
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public PostReadDbModel Post { get; set; }
}