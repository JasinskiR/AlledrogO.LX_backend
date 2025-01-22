using AlledrogO.Shared.Commands;

namespace AlledrogO.Message.Core.Commands;

public record CreateSpamMessages(int Count) : ICommand;