using AlledrogO.Post.Application.DTOs;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record CreateAuthor(AuthorDetailsDto AuthorDetails) : ICommand<Guid>;
