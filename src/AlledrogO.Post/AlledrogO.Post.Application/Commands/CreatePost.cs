using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record CreatePost(Guid Id, string Title, string Description, Guid AuthorId) : ICommand;