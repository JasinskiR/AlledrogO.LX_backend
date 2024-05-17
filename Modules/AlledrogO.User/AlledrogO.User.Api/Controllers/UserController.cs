using AlledrogO.User.Api.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlledrogO.User.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<Core.Entities.User> _userManager;
    private readonly SignInManager<Core.Entities.User> _signInManager;

    public UserController(UserManager<Core.Entities.User> userManager, SignInManager<Core.Entities.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
        
        if (result.Succeeded)
        {
            return Ok(new { Message = "Login successful" });
        }

        return Unauthorized();
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Message = "Logout successful" });
    }
}