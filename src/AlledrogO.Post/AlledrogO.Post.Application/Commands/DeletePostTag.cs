using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record DeletePostTag(Guid PostId, string Tag) : ICommand;