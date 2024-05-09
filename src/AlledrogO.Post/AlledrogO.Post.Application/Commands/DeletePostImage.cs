using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record DeletePostImage(Guid PostId, string ImageUrl) : ICommand;