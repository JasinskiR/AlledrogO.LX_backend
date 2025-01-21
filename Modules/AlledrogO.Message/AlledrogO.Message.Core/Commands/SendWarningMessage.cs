using AlledrogO.Shared.Commands;

namespace AlledrogO.Message.Core.Commands;

public record SendWarningMessage(string email, string message) : ICommand;