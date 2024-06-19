namespace AlledrogO.Message.Core.Entities;

public record Message
{
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; }
    public bool SentByBuyer { get; set; }
}