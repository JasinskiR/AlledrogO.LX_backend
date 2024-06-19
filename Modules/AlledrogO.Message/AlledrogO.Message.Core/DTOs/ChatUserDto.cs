namespace AlledrogO.Message.Core.DTOs;

public class ChatUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<ChatDto> ChatsAsBuyer { get; set; }
    public IEnumerable<ChatDto> ChatsAsAdvertiser { get; set; }
}