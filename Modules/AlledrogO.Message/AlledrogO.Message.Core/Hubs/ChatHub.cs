using AlledrogO.Message.Core.DTOs;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.SignalR;

namespace AlledrogO.Message.Core.Hubs;

public class ChatHub : Hub
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public ChatHub(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    public override async Task OnConnectedAsync()
    {
        var chatId = Context.GetHttpContext().Request.Query["chatId"];
        if (chatId.Count == 0)
        {
            await Clients.Caller.SendAsync("Error", "No chat id provided");
            return;
        }
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.FirstOrDefault());
        base.OnConnectedAsync();
    }
    
    // public async Task SendMessage(MessageDto messageDto, Guid chatId)
    // {
    //     await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", messageDto);
    // }

    // public async Task SendPrivateMessage(Guid chatId, string content)
    // {
    //     if (!Guid.TryParse(Context.UserIdentifier, out Guid senderId))
    //     {
    //         await Clients.Caller.SendAsync("Error", $"Invalid sender id: {Context.UserIdentifier}");
    //     }
    //     await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    //     
    //     var command = new AddMessageToChat(
    //         new IncomingMessageDto()
    //         {
    //             ChatId = chatId,
    //             Content = content
    //         }, senderId);
    //     try
    //     {
    //         await _commandDispatcher.DispatchAsync(command);
    //     }
    //     catch (AlledrogoException e)
    //     {
    //         await Clients.Caller.SendAsync("Error", e.Message);
    //     }
    //     
    //     await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", content, senderId);
    // }
}