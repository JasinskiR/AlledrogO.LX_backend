using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record AddPostImage(Guid PostId, string ImageUrl) : ICommand;