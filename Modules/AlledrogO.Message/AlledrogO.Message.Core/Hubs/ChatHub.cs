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
}