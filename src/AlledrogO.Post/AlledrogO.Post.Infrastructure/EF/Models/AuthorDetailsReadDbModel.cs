namespace AlledrogO.Post.Infrastructure.EF.Models;

public class AuthorDetailsReadDbModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public AuthorReadDbModel Author { get; set; }
}