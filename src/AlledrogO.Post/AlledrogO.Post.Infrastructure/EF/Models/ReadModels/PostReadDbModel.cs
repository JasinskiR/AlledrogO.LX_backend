using AlledrogO.Post.Domain.Consts;

namespace AlledrogO.Post.Infrastructure.EF.Models.ReadModels;

public class PostReadDbModel
{
    public Guid Id { get; set; }
    public uint Version { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<PostImageReadDbModel> Images { get; set; }
    public ICollection<TagReadDbModel> Tags { get; set; }
    public PostStatus Status { get; set; }
    public AuthorReadDbModel Author { get; set; }
    public AuthorDetailsReadDbModel AuthorDetails { get; set; }
}