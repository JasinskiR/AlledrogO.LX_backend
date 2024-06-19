using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Message.Core.Exceptions;

public class UnauthorizedChatException : AlledrogoException
{
    public UnauthorizedChatException() : base("You are not authorized to access this chat.")
    {
    }
}