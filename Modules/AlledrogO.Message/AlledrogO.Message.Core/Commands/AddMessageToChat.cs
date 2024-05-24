using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.DTOs.External;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Message.Core.Commands;

public record AddMessageToChat(IncomingMessageDto IncomingMessageDto, Guid SenderId) : ICommand;