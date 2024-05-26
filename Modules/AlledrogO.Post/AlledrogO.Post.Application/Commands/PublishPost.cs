using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record PublishPost(Guid PostId) : ICommand;