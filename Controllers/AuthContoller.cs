using capstone_policy_management.DTOs;
using capstone_policy_management.Filters;
using capstone_policy_management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capstone_policy_management.Controllers;

[ApiController]
[ServiceFilter(typeof(GlobalResponseFilter))]
[Route("api/auth")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (token, user) = await _authService.LoginAsync(dto);
        return Ok(new { token, user });
    }

    [AllowAnonymous]
    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (token, user) = await _authService.RegisterAsync(dto);
        return Ok(new 
        { 
            message = "Registration successful",
            token,
            user
        });
    }
}