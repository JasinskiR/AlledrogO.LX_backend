using System.ComponentModel.DataAnnotations;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record CreatePost(
    string Title,
    string Description,
    Guid AuthorId,
    AuthorDetailsDto? AuthorDetails) : ICommand<Guid>;

    
    