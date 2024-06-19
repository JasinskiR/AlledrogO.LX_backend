using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Message.Core.Exceptions;

public class ChatUserNotFoundException : AlledrogoException
{
    public ChatUserNotFoundException(Guid chatUserId) : base($"ChatUser with id {chatUserId} not found")
    {
    }
}