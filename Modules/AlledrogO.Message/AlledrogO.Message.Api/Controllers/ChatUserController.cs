using AlledrogO.Message.Core.Commands;
using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.Queries;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AlledrogO.Message.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatUserController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public ChatUserController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChatUserDto>>> Get()
    {
        var query = new GetChatUsers();
        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create()
    {
        var command = new CreateChatUser();
        var result = await _commandDispatcher.DispatchAsync<CreateChatUser, Guid>(command);
        return Ok(result);
    }
}