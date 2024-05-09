using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record AddTagToPost(Guid PostId, string TagName) : ICommand;