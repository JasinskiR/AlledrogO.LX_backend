using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<TagDto>>> Get()
    {
        var query = new GetTags();
        var result = await _queryDispatcher.QueryAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet("{Id:guid}")]
    public async Task<ActionResult<TagDto>> Get([FromRoute] GetTagById query)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}