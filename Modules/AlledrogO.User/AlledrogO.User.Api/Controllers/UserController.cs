using System.Security.Claims;
using AlledrogO.Shared.MassTransit.Events;
using AlledrogO.User.Api.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlledrogO.User.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<Core.Entities.User> _userManager;
    private readonly SignInManager<Core.Entities.User> _signInManager;
    private readonly IBus _bus;

    public UserController(UserManager<Core.Entities.User> userManager, 
        SignInManager<Core.Entities.User> signInManager, IBus bus)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _bus = bus;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new Core.Entities.User
        {
            UserName = dto.Email,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            var createdUser = await _userManager.FindByEmailAsync(user.Email);
            await _bus.Publish(new UserCreatedEvent()
            {
                UserId = new Guid(createdUser.Id),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
            return Ok(new { Message = "Registration successful" });
        }
        
        return BadRequest(new { Errors = result.Errors });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
        {
            return BadRequest(new { Message = "Invalid email or password" });
        }
        _signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;
        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);
        
        if (result.Succeeded)
        {
            return Ok(new { Message = "Login successful" });
        }

        return Unauthorized();
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Message = "Logout successful" });
    }
    
    [HttpGet("test")]
    [Authorize]
    public IActionResult Test()
    {
        return Ok($"Hello {User.Identity.Name}");
    }
    
    [HttpDelete("deleteAccount")]
    [Authorize]
    public async Task<IActionResult> DeleteAccount()
    {
        var user = await _userManager.FindByEmailAsync(User!.FindFirstValue(ClaimTypes.Email));
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            await _bus.Publish(new UserDeletedEvent()
            {
                UserId = new Guid(user.Id)
            });
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "Account deleted" });
        }
        return BadRequest(new { Errors = result.Errors });
    }
}