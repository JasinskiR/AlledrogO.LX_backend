namespace AlledrogO.Message.Core.DTOs;

public class ChatDto
{
    public Guid Id { get; set; }
    public string AdvertiserEmail { get; set; }
    public Guid AdvertiserId { get; set; }
    public string BuyerEmail { get; set; }
    public Guid BuyerId { get; set; }
    public IEnumerable<MessageDto> Messages { get; set; }
}