using System.Text.Json;
using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.Exceptions;
using AlledrogO.Message.Core.Hubs;
using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.Commands;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.SignalR;

namespace AlledrogO.Message.Core.Commands.Handlers;

public class AddMessageToChatHandler : ICommandHandler<AddMessageToChat>
{
    private readonly IChatRepository _chatRepository;
    private readonly IChatUserRepository _chatUserRepository;
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IAmazonSQS _sqsClient;
    private readonly string _queueUrl;
    public AddMessageToChatHandler(IChatRepository chatRepository, IHubContext<ChatHub> hubContext, 
        IAmazonSQS sqsClient, IChatUserRepository chatUserRepository)
    {
        _chatRepository = chatRepository;
        _hubContext = hubContext;
        _sqsClient = sqsClient;
        _chatUserRepository = chatUserRepository;
        _queueUrl = Environment.GetEnvironmentVariable("SQS_QUEUE_URL")
                    ?? throw new NullReferenceException("SQS_QUEUE_URL environment variable is not set");
    }

    public async Task HandleAsync(AddMessageToChat command)
    {
        var chatId = command.ChatId;
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

        var sender = await _chatUserRepository.GetByIdAsync(command.SenderId);
        if (sender == null)
        {
            throw new ChatUserNotFoundException(command.SenderId);
        }
        var messageJson = JsonSerializer.Serialize( new
        {
            senderEmail = sender.Email,
            chatId = chatId,
            createdAt = message.CreatedAt,
            content = message.Content
        });
        await _sqsClient.SendMessageAsync(_queueUrl, messageJson);
        chat.Messages.AddLast(message);
        await _chatRepository.UpdateAsync(chat);

        await _hubContext.Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message.AsDto());
    }
}