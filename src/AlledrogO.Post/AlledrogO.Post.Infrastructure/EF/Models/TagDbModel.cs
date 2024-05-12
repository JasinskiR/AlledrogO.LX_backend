using AlledrogO.Post.Domain.Entities;

namespace AlledrogO.Post.Infrastructure.EF.Models;

public class TagDbModel
{
    public Guid Id { get; set; }
    public uint Version { get; set; }
    public string Name { get; set; }
    public uint PostCount { get; set; }
    public ICollection<PostDbModel> Posts { get; set; }
    
    // public static TagDbModel Create(Tag tag)
    // {
    //     return new TagDbModel
    //     {
    //         Id = tag.Id,
    //         Version = tag.Version,
    //         Name = tag.Name,
    //         PostCount = tag.PostCount,
    //         Posts = tag.Posts.Select(p => PostDbModel.Create(p)).ToList(),
    //     };
    // }
}