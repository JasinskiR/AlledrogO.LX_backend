namespace AlledrogO.Post.Infrastructure.EF.Models;

public class AuthorDetailsReadDbModel
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public static AuthorDetailsReadDbModel Create(string value)
    {
        var parts = value.Split(", ");
        return new AuthorDetailsReadDbModel
        {
            Email = parts[0],
            PhoneNumber = parts[1]
        };
    }
    public override string ToString() => $"{Email}, {PhoneNumber}";
}