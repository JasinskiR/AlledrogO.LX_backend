namespace AlledrogO.Message.Core.DTOs;

public class MessageDto
{
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; }
    public bool SentByBuyer { get; set; }
}