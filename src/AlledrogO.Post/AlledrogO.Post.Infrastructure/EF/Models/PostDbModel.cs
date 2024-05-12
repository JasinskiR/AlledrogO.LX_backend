using System.Collections;
using AlledrogO.Post.Domain.Consts;

namespace AlledrogO.Post.Infrastructure.EF.Models;

public class PostDbModel
{
    public Guid Id { get; set; }
    public uint Version { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public PostStatus Status { get; set; }
    
    public string SharedAuthorDetails { get; set; }
    public ICollection<PostImageDbModel> Images { get; set; }
    public ICollection<TagDbModel> Tags { get; set; }
    public AuthorDbModel Author { get; set; }
    
    // public static PostDbModel Create(Domain.Entities.Post post)
    // {
    //     return new PostDbModel
    //     {
    //         Id = post.Id,
    //         Version = post.Version,
    //         Title = post.Title,
    //         Description = post.Description,
    //         Status = post.Status,
    //         SharedAuthorDetails = post.SharedAuthorDetails.ToString(),
    //         Images = post.Images.Select(i => PostImageDbModel.Create(i)).ToList(),
    //         Tags = post.Tags.Select(t => t.ToDbModel()).ToList(),
    //
    //         
    //     };
    // }
}