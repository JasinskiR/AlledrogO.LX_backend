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
        var validator = new RegisterDtoValidator();
        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(new { Errors = validationResult.Errors.FirstOrDefault()!.ErrorMessage });
        }
        
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
    public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
        {
            return TypedResults.Problem("User with this email does not exist", statusCode: StatusCodes.Status400BadRequest);
        }
        
        _signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;
        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);

        if (result.Succeeded)
        {
            return TypedResults.Empty;
        }

        return TypedResults.Problem("Invalid email or password", statusCode: StatusCodes.Status401Unauthorized);
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
        return Ok($"Hello {User.Identity.Name} with Id {User.FindFirstValue(ClaimTypes.NameIdentifier)}!");
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