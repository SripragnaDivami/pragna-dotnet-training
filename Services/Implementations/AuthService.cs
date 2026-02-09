
using capstone_policy_management.DTOs;
using capstone_policy_management.DTOs.AuthDTOs;
using capstone_policy_management.Entities;
using capstone_policy_management.Helper;
using capstone_policy_management.Repository.Interfaces;
using capstone_policy_management.Services.Interfaces;

namespace capstone_policy_management.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    
    public async Task<(string token, object user)> RegisterAsync(RegisterRequestDTO registerDto)
    {
        // Check if email already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email already exists");
        }

        // Hash password using BCrypt
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        // Create new user with default "User" role
        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            PasswordHash = hashedPassword,
            Role = "User",
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
        };

        var createdUser = await _userRepository.CreateUserAsync(user);

        // Generate JWT token
        var token = _jwtTokenGenerator.GenerateToken(createdUser);

        return (token, new
        {
            id = createdUser.Id,
            name = createdUser.Name,
            email = createdUser.Email,
            role = createdUser.Role
        });
    }

    public async Task<(string token, object user)> LoginAsync(LoginRequestDTO loginDto)
    {
        // Find user by email
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // Verify password
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // Generate JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return (token, new
        {
            id = user.Id,
            name = user.Name,
            email = user.Email,
            role = user.Role
        });
    }
}
