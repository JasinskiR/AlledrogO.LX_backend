using AlledrogO.Message.Core.Entities;
using AlledrogO.Message.Core.Exceptions;
using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Message.Core.Commands.Handlers;

public class AddMessageToChatHandler : ICommandHandler<AddMessageToChat>
{
    private readonly IChatRepository _chatRepository;

    public AddMessageToChatHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task HandleAsync(AddMessageToChat command)
    {
        var chatId = command.IncomingMessageDto.ChatId;
        var chat = await _chatRepository.GetByIdAsync(chatId);
        if (chat == null)
        {
            throw new ChatNotFoundException(chatId);
        }
        
        var message = new Entities.Message()
        {
            CreatedAt = DateTime.Now,
            Content = command.IncomingMessageDto.Content,
        };
        if (command.SenderId == chat.BuyerId)
        {
            message.SentByBuyer = true;
        }
        else if (command.SenderId == chat.AdvertiserId)
        {
            message.SentByBuyer = false;
        }
        else
        {
            throw new UnauthorizedChatException();
        }
            
        chat.Messages.AddLast(message);
        await _chatRepository.UpdateAsync(chat);
    }
}