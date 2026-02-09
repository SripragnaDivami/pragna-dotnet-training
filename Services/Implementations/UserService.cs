
using capstone_policy_management.DTOs.PolicyEnrollmentDTOs;
using capstone_policy_management.DTOs.UserDTOs;
using capstone_policy_management.Entities;
using capstone_policy_management.Repository.Interfaces;
using capstone_policy_management.Services.Interfaces;

namespace capstone_policy_management.Services.Implementations;


public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IPolicyEnrollmentRepository enrollmentRepository;

    public UserService(IUserRepository _userRepository, IPolicyEnrollmentRepository _enrollmentRepository)
    {
        userRepository = _userRepository;
        enrollmentRepository = _enrollmentRepository;
    }

    public async Task<List<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllUsersAsync();
        return users.Select(MapToResponseDto).ToList();
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        return user != null ? MapToResponseDto(user) : null;
    }

    public async Task<UserResponseDto> CreateUserAsync(UserCreateDto userDto)
    {
       
        var user = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        var createdUser = await userRepository.CreateUserAsync(user);
        return MapToResponseDto(createdUser);
    }

    public async Task<UserResponseDto?> UpdateUserAsync(int id, UserUpdateDto userDto)
    {
        
        var existingUser = await userRepository.GetUserByIdAsync(id);
        if (existingUser == null)
            return null;

        
        if (!string.IsNullOrEmpty(userDto.Name))
            existingUser.Name = userDto.Name;

        if (!string.IsNullOrEmpty(userDto.Email))
            existingUser.Email = userDto.Email;

        if (!string.IsNullOrEmpty(userDto.Password))
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

        var updatedUser = await userRepository.UpdateUserAsync(existingUser);
        return MapToResponseDto(updatedUser);
    }

    public async Task DeleteUserAsync(int id)
    {
        await userRepository.DeleteUserAsync(id);
    }

    private static UserResponseDto MapToResponseDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<List<PolicyEnrollmentResponseDto>> GetEnrollmentsByUserIdAsync(int userId)
    {
        var enrollments = await enrollmentRepository.GetAllEnrollmentsAsync();
        var userEnrollments = enrollments.Where(e => e.UserId == userId).ToList();

        return userEnrollments.Select(enrollment => new PolicyEnrollmentResponseDto
        {
            Id = enrollment.Id,
            UserId = enrollment.UserId,
            UserName = enrollment.User?.Name ?? string.Empty,
            UserEmail = enrollment.User?.Email ?? string.Empty,
            PolicyId = enrollment.PolicyId,
            PolicyCode = enrollment.Policy?.PolicyCode ?? string.Empty,
            PolicyName = enrollment.Policy?.Name ?? string.Empty,
            PremiumAmount = enrollment.Policy?.PremiumAmount ?? 0,
            Status = enrollment.Status,
            RequestedAt = enrollment.RequestedAt,
            ApprovedAt = enrollment.ApprovedAt
        }).ToList();
    }
}