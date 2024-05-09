namespace AlledrogO.Post.Infrastructure.EF.Models;

public class AuthorReadDbModel
{
    public Guid Id { get; set; }
    public ICollection<PostReadDbModel> Posts { get; set; }
    public AuthorDetailsReadDbModel Details { get; set; }
}