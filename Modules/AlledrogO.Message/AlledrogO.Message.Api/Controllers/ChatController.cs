// using System.Security.Claims;
// using AlledrogO.Message.Core.Commands;
// using AlledrogO.Message.Core.DTOs;
// using AlledrogO.Message.Core.DTOs.External;
// using AlledrogO.Message.Core.Queries;
// using AlledrogO.Shared.Commands;
// using AlledrogO.Shared.Queries;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace AlledrogO.Message.Api.Controllers;
//
// [ApiController]
// [Route("api/[controller]")]
// public class ChatController : ControllerBase
// {
//     private readonly IQueryDispatcher _queryDispatcher;
//     private readonly ICommandDispatcher _commandDispatcher;
//     
//     private Guid LoggedInUserId => new(User.FindFirstValue(ClaimTypes.NameIdentifier));
//     
//     public ChatController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
//     {
//         _queryDispatcher = queryDispatcher;
//         _commandDispatcher = commandDispatcher;
//     }
//     
//     [HttpGet]
//     [Authorize]
//     [SwaggerOperation("Get all chats for logged in user")]
//     public async Task<ActionResult<IEnumerable<ChatDto>>> GetChatsForUser()
//     {
//         var query = new GetChatsForUser(LoggedInUserId);
//         var result = await _queryDispatcher.QueryAsync(query);
//         if (result is null)
//         {
//             return NotFound();
//         }
//         return Ok(result);
//     }
//     
//     [HttpPatch]
//     [Authorize]
//     public async Task<ActionResult> AddMessageToChat([FromBody] IncomingMessageDto incomingMessageDto)
//     {
//         var command = new AddMessageToChat(incomingMessageDto, LoggedInUserId);
//         await _commandDispatcher.DispatchAsync(command);
//         return Ok();
//     }
//     
// }