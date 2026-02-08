using capstone_policy_management.DTOs;
using capstone_policy_management.DTOs.PolicyEnrollmentDTOs;
using capstone_policy_management.DTOs.UserDTOs;

namespace capstone_policy_management.Services.Interfaces;


public interface IUserService
{
    Task<List<UserResponseDto>> GetAllUsersAsync();
    Task<UserResponseDto?> GetUserByIdAsync(int id);
    Task<UserResponseDto> CreateUserAsync(UserCreateDto userDto);
    Task<UserResponseDto?> UpdateUserAsync(int id, UserUpdateDto userDto);
    Task<List<PolicyEnrollmentResponseDto>> GetEnrollmentsByUserIdAsync(int userId);
    Task DeleteUserAsync(int id);
}