using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record DeletePostImage(Guid PostId, Guid ImageId) : ICommand;