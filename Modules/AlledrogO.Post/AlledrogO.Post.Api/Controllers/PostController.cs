using AlledrogO.Post.Application.Commands;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

    [HttpGet]
    [SwaggerOperation("Get all post cards for home page.")]
    public async Task<ActionResult<IEnumerable<PostCardDto>>> Get()
    {
        var query = new GetPostCards();
        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }
    

    [HttpGet("{Id:guid}")]
    [SwaggerOperation("Get post by ID (details view).")]
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
    [SwaggerOperation("Search posts.", 
        "Returns post cards with search query in title or description. You can provide tags to filter posts.")]
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
    [SwaggerOperation("Create post.")]
    public async Task<IActionResult> Post([FromBody] CreatePost command)
    {
        var result = await _commandDispatcher.DispatchAsync<CreatePost, Guid>(command);
        return CreatedAtAction(nameof(Get), new { id = result }, null);
    }
    
    [HttpPut("{Id:guid}/Image")]
    [SwaggerOperation("Upload image for post in jpg or png format.")]
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
    [SwaggerOperation("Add tag to post.")]
    public async Task<ActionResult> PutTag([FromRoute] AddTagToPost command)
    {
        var result = await _commandDispatcher.DispatchAsync<AddTagToPost, Guid>(command);
        return CreatedAtAction(nameof(Get), new { Id = result }, result);
    }
    
    [HttpDelete("{PostId:guid}/Tag/{TagName}")]
    [SwaggerOperation("Delete tag from post.")]
    public async Task<ActionResult> DeleteTag([FromRoute] DeleteTagFromPost command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
}