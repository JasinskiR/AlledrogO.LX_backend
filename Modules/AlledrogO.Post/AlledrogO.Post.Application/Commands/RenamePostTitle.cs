using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record RenamePostTitle(Guid PostId, string Title) : ICommand;