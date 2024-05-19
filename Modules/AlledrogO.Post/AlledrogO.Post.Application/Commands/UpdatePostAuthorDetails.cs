using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record UpdatePostAuthorDetails(Guid PostId, string AuthorEmail, string AuthorPhoneNumber) : ICommand;