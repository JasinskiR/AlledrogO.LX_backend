using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record AddPostTag(Guid PostId, string Tag) : ICommand;