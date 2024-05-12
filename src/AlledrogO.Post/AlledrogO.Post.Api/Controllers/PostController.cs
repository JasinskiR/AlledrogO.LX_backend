using AlledrogO.Post.Application.Commands;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AlledrogO.Post.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public PostController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpGet("{Id:guid}")]
    public async Task<ActionResult<PostDto>> Get([FromRoute] GetPostById query)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpPost("Search")]
    public async Task<ActionResult<IEnumerable<PostDto>>> Search([FromBody] SearchPosts query)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePost command)
    {
        var result = await _commandDispatcher.DispatchAsync<CreatePost, Guid>(command);
        return CreatedAtAction(nameof(Get), new { id = result }, null);
    }
    
    [HttpPost("{Id:guid}/Image")]
    public async Task<IActionResult> UploadImage([FromRoute] Guid Id, IFormFile file)
    {
        var command = new AddPostImage(Id, file);

        var result = await _commandDispatcher.DispatchAsync<AddPostImage, string>(command);
        if (result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
}