using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record UpdatePostDescription(Guid PostId, string Description) : ICommand;