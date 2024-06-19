namespace AlledrogO.Message.Core.DTOs;

public class ChatUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<Guid> ChatsAsBuyer { get; set; }
    public IEnumerable<Guid> ChatsAsAdvertiser { get; set; }
}