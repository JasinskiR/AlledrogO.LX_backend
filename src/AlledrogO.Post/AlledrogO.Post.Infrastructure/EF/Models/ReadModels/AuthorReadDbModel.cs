namespace AlledrogO.Post.Infrastructure.EF.Models.ReadModels;

public class AuthorReadDbModel
{
    public Guid Id { get; set; }
    public ICollection<PostReadDbModel> Posts { get; set; }
    public AuthorDetailsReadDbModel Details { get; set; }
}