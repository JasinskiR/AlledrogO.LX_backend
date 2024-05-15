using System.ComponentModel.DataAnnotations;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record CreatePost(
    string Title,
    string Description,
    Guid AuthorId) : ICommand<Guid>;

    
    