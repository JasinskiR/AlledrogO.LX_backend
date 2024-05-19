using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands;

public record DeleteAuthor(Guid Id) : ICommand;