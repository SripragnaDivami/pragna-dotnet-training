using capstone_policy_management.DTOs.PolicyEnrollmentDTOs;

namespace capstone_policy_management.Services.Interfaces;

public interface IPolicyEnrollmentService
{
    Task<IEnumerable<PolicyEnrollmentResponseDto>> GetAllEnrollmentsAsync();
    Task<PolicyEnrollmentResponseDto?> GetEnrollmentByIdAsync(int id);
    Task<PolicyEnrollmentResponseDto> CreateEnrollmentAsync(PolicyEnrollmentCreateDto enrollmentDto);
    Task<PolicyEnrollmentResponseDto?> UpdateEnrollmentAsync(int id, PolicyEnrollmentUpdateDto enrollmentDto);
    Task<PolicyEnrollmentResponseDto?> ApproveEnrollmentAsync(int id);
    Task<PolicyEnrollmentResponseDto?> RejectEnrollmentAsync(int id);
    Task<IEnumerable<PolicyEnrollmentResponseDto>> GetEnrollmentsByStatusAsync(string status);
    Task DeleteEnrollmentAsync(int id);
}