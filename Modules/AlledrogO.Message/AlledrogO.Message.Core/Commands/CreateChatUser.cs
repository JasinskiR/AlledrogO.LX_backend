using AlledrogO.Shared.Commands;

namespace AlledrogO.Message.Core.Commands;

public record CreateChatUser() : ICommand<Guid>;