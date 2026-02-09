using capstone_policy_management.DTOs;
using capstone_policy_management.DTOs.PolicyEnrollmentDTOs;
using capstone_policy_management.DTOs.UserDTOs;
using capstone_policy_management.Filters;
using capstone_policy_management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capstone_policy_management.Controllers;

[ApiController]
[ServiceFilter(typeof(GlobalResponseFilter))]
[Route("api/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService _userService)
    {
        userService = _userService;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet(Name = "GetAllUsers")]
    public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserResponseDto>> GetUserById(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { message = $"User with ID {id} not found." });
        }
        return Ok(user);
    }

    [Authorize(Roles = "User")]
    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] UserCreateDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdUser = await userService.CreateUserAsync(userDto);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    [Authorize(Roles = "User")]
    [HttpPut("{id}", Name = "UpdateUser")]
    public async Task<ActionResult<UserResponseDto>> UpdateUser(int id, [FromBody] UserUpdateDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedUser = await userService.UpdateUserAsync(id, userDto);
        if (updatedUser == null)
        {
            return NotFound(new { message = $"User with ID {id} not found." });
        }

        return Ok(updatedUser);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var existingUser = await userService.GetUserByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound(new { message = $"User with ID {id} not found." });
        }

        await userService.DeleteUserAsync(id);
        return Ok(new { message = "User deleted successfully." });
    }

    [Authorize(Roles = "User")]
    [HttpGet("my/enrollments", Name = "GetMyEnrollments")]
    public async Task<ActionResult<List<PolicyEnrollmentResponseDto>>> GetMyEnrollments([FromQuery] int userId)
    {
        var enrollments = await userService.GetEnrollmentsByUserIdAsync(userId);
        return Ok(enrollments);
    }
}