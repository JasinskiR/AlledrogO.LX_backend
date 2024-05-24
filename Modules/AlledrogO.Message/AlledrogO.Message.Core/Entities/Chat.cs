
using System.ComponentModel.DataAnnotations.Schema;

namespace AlledrogO.Message.Core.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public ChatUser Buyer { get; set; }
    public Guid BuyerId { get; set; }
    public ChatUser Advertiser { get; set; }
    public Guid AdvertiserId { get; set; }
    
    public LinkedList<Message> Messages { get; set; }
}