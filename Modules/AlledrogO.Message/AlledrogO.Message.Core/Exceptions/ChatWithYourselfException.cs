using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Message.Core.Exceptions;

public class ChatWithYourselfException : AlledrogoException
{
    public ChatWithYourselfException() : base("You can't chat with yourself.")
    {
    }
}