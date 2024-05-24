using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.Exceptions;
using AlledrogO.Message.Core.Hubs;
using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.Commands;
using Microsoft.AspNetCore.SignalR;

namespace AlledrogO.Message.Core.Commands.Handlers;

public class AddMessageToChatHandler : ICommandHandler<AddMessageToChat>
{
    private readonly IChatRepository _chatRepository;
    private readonly IHubContext<ChatHub> _hubContext;
    public AddMessageToChatHandler(IChatRepository chatRepository, IHubContext<ChatHub> hubContext)
    {
        _chatRepository = chatRepository;
        _hubContext = hubContext;
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
        await _hubContext.Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message.AsDto());
    }
}