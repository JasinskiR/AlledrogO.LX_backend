using System.Collections;

namespace AlledrogO.Post.Infrastructure.EF.Models;

public class AuthorDbModel
{
    public Guid Id { get; set; }
    public uint Version { get; set; }
    public string AuthorDetails { get; set; }
    public ICollection<PostDbModel> Posts { get; set; }
}