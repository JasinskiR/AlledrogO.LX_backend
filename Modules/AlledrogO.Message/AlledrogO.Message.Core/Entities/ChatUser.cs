namespace AlledrogO.Message.Core.Entities;

public class ChatUser
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public ICollection<Chat> ChatsAsBuyer { get; set; }
    public ICollection<Chat> ChatsAsAdvertiser { get; set; }
    
}