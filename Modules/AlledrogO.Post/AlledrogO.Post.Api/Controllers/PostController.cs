using System.Security.Claims;
using AlledrogO.Post.Application.Commands;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.DTOs.External;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Post.Application.Services;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IAuthorPermissionService _permissionService;
    private Guid LoggedInUserId => new(User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    public PostController(IQueryDispatcher queryDispatcher, 
        ICommandDispatcher commandDispatcher, IAuthorPermissionService permissionService)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _permissionService = permissionService;
    }

    [HttpGet]
    [SwaggerOperation("Get all post cards for home page (only published)")]
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
    [SwaggerOperation("Search for posts (only published).", 
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
    [Authorize]
    [SwaggerOperation("Create post.")]
    public async Task<IActionResult> Post([FromBody] CreatePostDto dto)
    {
        var command = new CreatePost(dto.Title, dto.Description, LoggedInUserId);
        
        var result = await _commandDispatcher.DispatchAsync<CreatePost, Guid>(command);
        return CreatedAtAction(nameof(Get), new { id = result }, null);
    }
    
    [HttpPut("{PostId:guid}/Image")]
    [SwaggerOperation("Upload image for post in jpg or png format.")]
    [Authorize]
    public async Task<IActionResult> UploadImage([FromRoute] Guid PostId, IFormFile file)
    {
        if (await _permissionService.CanEditPostAsync(LoggedInUserId, PostId) is false)
        {
            return Forbid();
        }
        var command = new AddPostImage(PostId, file);

        var result = await _commandDispatcher.DispatchAsync<AddPostImage, string>(command);
        if (result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    
    [HttpDelete("{PostId:guid}/Image/{ImageId:guid}")]
    [SwaggerOperation("Delete image from post.")]
    [Authorize]
    public async Task<IActionResult> DeleteImage([FromRoute] DeletePostImage command)
    {
        if (await _permissionService.CanEditPostAsync(LoggedInUserId, command.PostId) is false)
        {
            return Forbid();
        }
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
    
    [HttpPut("{PostId:guid}/Tag/{TagName}")]
    [SwaggerOperation("Add tag to post.")]
    public async Task<ActionResult> PutTag([FromRoute] AddTagToPost command)
    {
        if (await _permissionService.CanEditPostAsync(LoggedInUserId, command.PostId) is false)
        {
            return Forbid();
        }
        var result = await _commandDispatcher.DispatchAsync<AddTagToPost, Guid>(command);
        return CreatedAtAction(nameof(Get), new { Id = result }, result);
    }
    
    [HttpDelete("{PostId:guid}/Tag/{TagName}")]
    [SwaggerOperation("Delete tag from post.")]
    public async Task<ActionResult> DeleteTag([FromRoute] DeleteTagFromPost command)
    {
        if (await _permissionService.CanEditPostAsync(LoggedInUserId, command.PostId) is false)
        {
            return Forbid();
        }
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
}