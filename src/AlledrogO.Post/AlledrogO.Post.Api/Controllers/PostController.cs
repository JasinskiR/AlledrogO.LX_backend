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
    
    /// <summary>
    /// Retrieves all post cards for home page.
    /// </summary>
    /// <returns>Collection of post cards (id, title, image)</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostCardDto>>> Get()
    {
        var query = new GetPostCards();
        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }
    
    /// <summary>
    /// Retrieves post by id.
    /// </summary>
    /// <param name="query"> requires Guid as a post Id </param>
    /// <returns> Post with specified Id </returns>
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
    
    /// <summary>
    /// Search posts cards by title or description and tags
    /// </summary>
    /// <param name="query"> Query containing searchphrase and list of tags </param>
    /// <returns> Collection of post cards (id, title, image)</returns>
    [HttpPost("Search")]
    public async Task<ActionResult<IEnumerable<PostCardDto>>> Search([FromBody] SearchPosts query)
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
    
    [HttpPut("{PostId:guid}/Tag/{TagName}")]
    public async Task<ActionResult> PutTag([FromRoute] AddTagToPost command)
    {
        var result = await _commandDispatcher.DispatchAsync<AddTagToPost, Guid>(command);
        return CreatedAtAction(nameof(Get), new { Id = result }, result);
    }
    
    [HttpDelete("{PostId:guid}/Tag/{TagName}")]
    public async Task<ActionResult> DeleteTag([FromRoute] DeleteTagFromPost command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
}