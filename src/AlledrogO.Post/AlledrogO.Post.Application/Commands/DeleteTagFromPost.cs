using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record DeleteTagFromPost(Guid PostId, string TagName) : ICommand;