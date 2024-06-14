using System.Security.Claims;
using AlledrogO.Post.Application.Commands;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AlledrogO.Post.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    private Guid LoggedInUserId => new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier) 
                                            ?? Guid.Empty.ToString());
    public AuthorController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpGet]
    [SwaggerOperation("ONLY FOR TESTING PURPOSE. Get all authors")]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> Get()
    {
        var query = new GetAuthors();
        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet("applyMigrationsManually")]
    [SwaggerOperation("ONLY FOR TESTING PURPOSE. Apply migrations manually")]
    public async Task<ActionResult> ApplyMigrationsManually()
    {
        var command = new ApplyMigrationsManually();
        await _queryDispatcher.QueryAsync(command);
        return Ok();
    }
    
    [HttpGet("info")]
    [SwaggerOperation("Get info about logged author")]
    [Authorize]
    public async Task<ActionResult<AuthorDto>> GetInfo()
    {
        var query = new GetAuthorById(LoggedInUserId);
        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet("posts")]
    [SwaggerOperation("Get all posts of logged author (all statuses)")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<PostCardDto>>> GetPosts()
    {
        var query = new GetPostCardsByAuthor(LoggedInUserId);
        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    
    // [HttpGet("{Id:guid}")]
    // [SwaggerOperation("Get author by ID")]
    // public async Task<ActionResult<AuthorDto>> GetById([FromRoute] GetAuthorById query)
    // {
    //     var result = await _queryDispatcher.QueryAsync(query);
    //     
    //     if (result is null)
    //     {
    //         return NotFound();
    //     }
    //     return Ok(result);
    // }
}