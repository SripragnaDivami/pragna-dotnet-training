using capstone_policy_management.DTOs.PolicyEnrollmentDTOs;
using capstone_policy_management.Entities;
using capstone_policy_management.Repository.Interfaces;
using capstone_policy_management.Services.Interfaces;

namespace capstone_policy_management.Services.Implementations;


public class PolicyEnrollmentService : IPolicyEnrollmentService
{
    private readonly IPolicyEnrollmentRepository enrollmentRepository;

    public PolicyEnrollmentService(IPolicyEnrollmentRepository enrollmentRepository)
    {
        this.enrollmentRepository = enrollmentRepository;
    }

    public async Task<IEnumerable<PolicyEnrollmentResponseDto>> GetAllEnrollmentsAsync()
    {
        var enrollments = await enrollmentRepository.GetAllEnrollmentsAsync();
        return enrollments.Select(e => new PolicyEnrollmentResponseDto
        {
            Id = e.Id,
            UserId = e.UserId,
            UserName = e.User?.Name ?? string.Empty,
            UserEmail = e.User?.Email ?? string.Empty,
            PolicyId = e.PolicyId,
            PolicyCode = e.Policy?.PolicyCode ?? string.Empty,
            PolicyName = e.Policy?.Name ?? string.Empty,
            PremiumAmount = e.Policy?.PremiumAmount ?? 0,
            Status = e.Status,
            RequestedAt = e.RequestedAt,
            ApprovedAt = e.ApprovedAt
        });
    }


    public async Task<IEnumerable<PolicyEnrollmentResponseDto>> GetEnrollmentsByStatusAsync(string status)
    {
        var enrollments = await enrollmentRepository.GetEnrollmentsByStatusAsync(status);
        return enrollments.Select(e => new PolicyEnrollmentResponseDto
        {
            Id = e.Id,
            UserId = e.UserId,
            UserName = e.User?.Name ?? string.Empty,
            UserEmail = e.User?.Email ?? string.Empty,
            PolicyId = e.PolicyId,
            PolicyCode = e.Policy?.PolicyCode ?? string.Empty,
            PolicyName = e.Policy?.Name ?? string.Empty,
            PremiumAmount = e.Policy?.PremiumAmount ?? 0,
            Status = e.Status,
            RequestedAt = e.RequestedAt,
            ApprovedAt = e.ApprovedAt
        });
    }

    public async Task<PolicyEnrollmentResponseDto?> GetEnrollmentByIdAsync(int id)
    {
        var enrollment = await enrollmentRepository.GetEnrollmentByIdAsync(id);
        if (enrollment == null) return null;

        return new PolicyEnrollmentResponseDto
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
        };
    }

    public async Task<PolicyEnrollmentResponseDto> CreateEnrollmentAsync(PolicyEnrollmentCreateDto enrollmentDto)
    {
        // Check if user already enrolled in this policy
        var existingEnrollment = await enrollmentRepository.GetEnrollmentByUserAndPolicyAsync(
            enrollmentDto.UserId, 
            enrollmentDto.PolicyId);
        
        if (existingEnrollment != null)
        {
            throw new InvalidOperationException("You are already enrolled in this policy");
        }

        var enrollment = new PolicyEnrollment
        {
            UserId = enrollmentDto.UserId,
            PolicyId = enrollmentDto.PolicyId,
            Status = "Pending",
            RequestedAt = DateTime.UtcNow
        };

        var created = await enrollmentRepository.CreateEnrollmentAsync(enrollment);

        return new PolicyEnrollmentResponseDto
        {
            Id = created.Id,
            UserId = created.UserId,
            UserName = created.User?.Name ?? string.Empty,
            UserEmail = created.User?.Email ?? string.Empty,
            PolicyId = created.PolicyId,
            PolicyCode = created.Policy?.PolicyCode ?? string.Empty,
            PolicyName = created.Policy?.Name ?? string.Empty,
            PremiumAmount = created.Policy?.PremiumAmount ?? 0,
            Status = created.Status,
            RequestedAt = created.RequestedAt,
            ApprovedAt = created.ApprovedAt
        };
    }

    public async Task<PolicyEnrollmentResponseDto?> UpdateEnrollmentAsync(int id, PolicyEnrollmentUpdateDto enrollmentDto)
    {
        var existing = await enrollmentRepository.GetEnrollmentByIdAsync(id);
        if (existing == null) return null;

        if (!string.IsNullOrEmpty(enrollmentDto.Status))
        {
            existing.Status = enrollmentDto.Status;
        }

        if (enrollmentDto.ApprovedAt.HasValue)
        {
            existing.ApprovedAt = enrollmentDto.ApprovedAt.Value;
        }

        var updated = await enrollmentRepository.UpdateEnrollmentAsync(existing);

        return new PolicyEnrollmentResponseDto
        {
            Id = updated.Id,
            UserId = updated.UserId,
            UserName = updated.User?.Name ?? string.Empty,
            UserEmail = updated.User?.Email ?? string.Empty,
            PolicyId = updated.PolicyId,
            PolicyCode = updated.Policy?.PolicyCode ?? string.Empty,
            PolicyName = updated.Policy?.Name ?? string.Empty,
            PremiumAmount = updated.Policy?.PremiumAmount ?? 0,
            Status = updated.Status,
            RequestedAt = updated.RequestedAt,
            ApprovedAt = updated.ApprovedAt
        };
    }

    public async Task DeleteEnrollmentAsync(int id)
    {
        await enrollmentRepository.DeleteEnrollmentAsync(id);
    }

    public async Task<PolicyEnrollmentResponseDto?> ApproveEnrollmentAsync(int id)
    {
        var updated = await enrollmentRepository.ApproveEnrollmentAsync(id);
        if (updated == null) return null;

        return new PolicyEnrollmentResponseDto
        {
            Id = updated.Id,
            UserId = updated.UserId,
            UserName = updated.User?.Name ?? string.Empty,
            UserEmail = updated.User?.Email ?? string.Empty,
            PolicyId = updated.PolicyId,
            PolicyCode = updated.Policy?.PolicyCode ?? string.Empty,
            PolicyName = updated.Policy?.Name ?? string.Empty,
            PremiumAmount = updated.Policy?.PremiumAmount ?? 0,
            Status = updated.Status,
            RequestedAt = updated.RequestedAt,
            ApprovedAt = updated.ApprovedAt
        };
    }


    public async Task<PolicyEnrollmentResponseDto?> RejectEnrollmentAsync(int id)
    {
        var updated = await enrollmentRepository.RejectEnrollmentAsync(id);
        if (updated == null) return null;

        return new PolicyEnrollmentResponseDto
        {
            Id = updated.Id,
            UserId = updated.UserId,
            UserName = updated.User?.Name ?? string.Empty,
            UserEmail = updated.User?.Email ?? string.Empty,
            PolicyId = updated.PolicyId,
            PolicyCode = updated.Policy?.PolicyCode ?? string.Empty,
            PolicyName = updated.Policy?.Name ?? string.Empty,
            PremiumAmount = updated.Policy?.PremiumAmount ?? 0,
            Status = updated.Status,
            RequestedAt = updated.RequestedAt,
            ApprovedAt = updated.ApprovedAt
        };
    }

}