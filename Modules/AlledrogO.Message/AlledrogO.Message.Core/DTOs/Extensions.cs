using AlledrogO.Message.Core.Entities;

namespace AlledrogO.Message.Core.DTOs;

internal static class Extensions
{
    public static ChatUserDto AsDto(this ChatUser chatUser)
    {
        return new ChatUserDto
        {
            Id = chatUser.Id,
            Email = chatUser.Email,
            ChatsAsBuyer = chatUser.ChatsAsBuyer?.Select(chat => chat.Id) ?? new List<Guid>(),
            ChatsAsAdvertiser = chatUser.ChatsAsAdvertiser?.Select(chat => chat.Id) ?? new List<Guid>()
        };
    }
    
    public static ChatDto AsDto(this Chat chat)
    {
        return new ChatDto
        {
            Id = chat.Id,
            AdvertiserEmail = chat.Advertiser.Email,
            AdvertiserId = chat.AdvertiserId,
            BuyerEmail = chat.Buyer.Email,
            BuyerId = chat.BuyerId,
            Messages = chat.Messages?.Select(message => message.AsDto()) ?? new List<MessageDto>()
        };
    }
    
    public static MessageDto AsDto(this Entities.Message message)
    {
        return new MessageDto
        {
            CreatedAt = message.CreatedAt,
            Content = message.Content,
            SentByBuyer = message.SentByBuyer
        };
    }
}