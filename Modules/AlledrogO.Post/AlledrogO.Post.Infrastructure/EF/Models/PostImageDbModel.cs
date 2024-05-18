using System.ComponentModel.DataAnnotations.Schema;
using AlledrogO.Post.Domain.ValueObjects;

namespace AlledrogO.Post.Infrastructure.EF.Models;

public class PostImageDbModel
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public PostDbModel Post { get; set; }
}