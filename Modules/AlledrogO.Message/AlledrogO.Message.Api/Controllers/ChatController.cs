using System.Security.Claims;
using AlledrogO.Message.Core.Commands;
using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.DTOs.External;
using AlledrogO.Message.Core.Queries;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlledrogO.Message.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    
    private Guid LoggedInUserId => new(User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    public ChatController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpGet("{UserId:Guid}")]
    public async Task<ActionResult<IEnumerable<ChatDto>>> GetChatsForUser([FromRoute] Guid UserId)
    {
        var query = new GetChatsForUser(UserId);
        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateChat([FromBody] CreateChat command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
    
    [HttpPatch]
    [Authorize]
    public async Task<ActionResult> AddMessageToChat([FromBody] IncomingMessageDto incomingMessageDto)
    {
        var command = new AddMessageToChat(incomingMessageDto, LoggedInUserId);
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
    
}