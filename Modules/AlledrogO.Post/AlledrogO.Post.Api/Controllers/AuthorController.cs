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
    
    [HttpGet("{Id:guid}")]
    [SwaggerOperation("Get author by ID")]
    public async Task<ActionResult<AuthorDto>> GetById([FromRoute] GetAuthorById query)
    {
        var result = await _queryDispatcher.QueryAsync(query);
        
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpDelete("{Id:guid}")]
    [SwaggerOperation("ONLY FOR TESTING PURPOSE. Delete author.", 
            "Author deletion should be done automatically " +
            "when deleting a user in user module. Should delete all posts by author.")]
    public async Task<ActionResult> Delete([FromRoute] DeleteAuthor command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
}