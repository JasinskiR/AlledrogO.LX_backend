using System.Security.Claims;
using AlledrogO.Message.Core.Commands;
using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.DTOs.External;
using AlledrogO.Message.Core.Queries;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AlledrogO.Message.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatUserController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    
    private Guid LoggedInUserId => new(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public ChatUserController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpGet]
    [SwaggerOperation("ONLY FOR TESTING PURPOSE. Get all chat users")]
    public async Task<ActionResult<IEnumerable<ChatUserDto>>> GetAll()
    {
        var query = new GetChatUsers();
        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet("info")]
    [Authorize]
    [SwaggerOperation("Get data about logged in chatUser")]
    public async Task<ActionResult<ChatUserDto>> GetById()
    {
        var query = new GetChatUserById(LoggedInUserId);
        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpPost("chats")]
    [Authorize]
    [SwaggerOperation("Create chat with given user")]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatDto createChatDto)
    {
        var command = new CreateChat(BuyerId: LoggedInUserId, AdvertiserId: createChatDto.RecieverId);
        var result = await _commandDispatcher.DispatchAsync<CreateChat, Guid>(command);
        if (result == Guid.Empty)
        {
            return BadRequest();
        }
        return Ok(new { ChatId = result });
    }
    
    [HttpGet("chats/{ChatId}")]
    [Authorize]
    [SwaggerOperation("Get chat with given id")]
    public async Task<ActionResult<ChatDetailsDto>> GetChatById(Guid ChatId)
    {
        var query = new GetChatById(ChatId);
        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpPatch("chats/{ChatId}")]
    [Authorize]
    [SwaggerOperation("Add message to chat")]
    public async Task<ActionResult> AddMessageToChat([FromBody] IncomingMessageDto incomingMessageDto, [FromRoute] Guid ChatId)
    {
        var command = new AddMessageToChat(ChatId, incomingMessageDto, LoggedInUserId);
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
    
    [HttpGet("test-message")]
    [SwaggerOperation("ONLY FOR TESTING PURPOSE. Send test message to SQS")]
    public async Task<ActionResult> TestMessage()
    {
        var command = new AddTestSqsMessage();
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
}