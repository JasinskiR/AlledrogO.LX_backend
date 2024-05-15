using AlledrogO.Post.Application.Commands;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Application.Queries;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AlledrogO.Post.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public AuthorController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpGet]
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
    
    
    [HttpGet("{Id:guid}")]
    public async Task<ActionResult<AuthorDto>> Get([FromRoute] GetAuthorById query)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    /// <summary>
    /// Only for testing purposes. Author creation should be done
    /// automatically when creating a user in user module.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateAuthor command)
    {
        var result = await _commandDispatcher.DispatchAsync<CreateAuthor, Guid>(command);
        return Ok(result);
    }
    
    /// <summary>
    /// Only for testing purposes. Author deletion should be done
    /// automatically when deleting a user in user module.
    /// Should delete all posts by author.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete("{Id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] DeleteAuthor command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
}