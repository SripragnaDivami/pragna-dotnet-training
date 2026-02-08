using capstone_policy_management.DTOs;

namespace capstone_policy_management.Services.Interfaces;

public interface IAuthService
{
    Task<(string token, object user)> RegisterAsync(RegisterRequestDTO registerDto);
    Task<(string token, object user)> LoginAsync(LoginRequestDTO loginDto);
}
