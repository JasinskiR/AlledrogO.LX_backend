using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record ArchivePost(Guid PostId) : ICommand;