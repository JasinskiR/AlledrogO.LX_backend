using AlledrogO.Post.Application.Commands;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AlledrogO.Post.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public TagController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpGet]
    [SwaggerOperation("Get all tags (only name and post count) ordered by popularity")]
    public async Task<ActionResult<IEnumerable<TagDto>>> Get()
    {
        var result = await _queryDispatcher.QueryAsync(new GetTags());
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet("{Id:guid}")]
    [SwaggerOperation("Get tag by ID")]
    public async Task<ActionResult<TagDetailsDto>> Get([FromRoute] GetTagById query)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet("{Name}")]
    [SwaggerOperation("Get tag by name")]
    public async Task<ActionResult<TagDetailsDto>> Get([FromRoute] GetTagByName query)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}