using AlledrogO.Post.Application.DTOs;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record UpdatePost(
    Guid PostId,
    string Title,
    string Description,
    AuthorDetailsDto? AuthorDetails) : ICommand;