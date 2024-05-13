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
        return Ok(result);
    }
    
    
}