using System.Security.Claims;
using AlledrogO.Shared.MassTransit.Events;
using AlledrogO.User.Api.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlledrogO.User.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IBus _bus;

    public UserController(IBus bus)
    {
        _bus = bus;
    }
    
    [HttpPost("register-event")]
    public async Task<IActionResult> Register([FromBody] RegisterEventDto dto)
    {
        await _bus.Publish(new UserCreatedEvent()
        {
            UserId = dto.Id,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        });
        return Ok(new { Message = "Registration event published" });
    }
    
    [HttpGet("test")]
    [Authorize]
    public IActionResult Test()
    {
        return Ok($"Hello {User.Identity.Name} with Id {User.FindFirstValue(ClaimTypes.NameIdentifier)}!");
    } }