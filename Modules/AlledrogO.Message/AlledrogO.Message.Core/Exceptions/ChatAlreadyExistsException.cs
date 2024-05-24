using AlledrogO.Shared.Exceptions;

namespace AlledrogO.Message.Core.Exceptions;

public class ChatAlreadyExistsException : AlledrogoException
{
    public ChatAlreadyExistsException(Guid buyerId, Guid advertiserId) 
        : base($"Chat between {buyerId} and {advertiserId} already exists.")
    {
    }
}