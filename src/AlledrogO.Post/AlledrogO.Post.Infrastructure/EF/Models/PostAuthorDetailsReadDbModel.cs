namespace AlledrogO.Post.Infrastructure.EF.Models;

public class PostAuthorDetailsReadDbModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public PostReadDbModel Post { get; set; }
}