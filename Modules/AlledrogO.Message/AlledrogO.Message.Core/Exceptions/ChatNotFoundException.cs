using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Message.Core.Exceptions;

public class ChatNotFoundException : AlledrogoException
{
    public ChatNotFoundException(Guid chatId) : base($"Chat with id {chatId} not found.")
    {
    }
}