using AlledrogO.Post.Application.DTOs;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record CreatePostWithCustomDetails(
    Guid Id, 
    string Title,
    string Description, 
    Guid AuthorId, 
    AuthorDetailsDto AuthorDetails) 
    : ICommand;