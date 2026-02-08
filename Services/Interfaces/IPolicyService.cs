using capstone_policy_management.DTOs;
using capstone_policy_management.DTOs.PolicyDTOs;

namespace capstone_policy_management.Services.Interfaces;

public interface IPolicyService
{
    Task<IEnumerable<PolicyResponseDto>> GetPoliciesAsync();
    Task<PolicyResponseDto?> GetPolicyByIdAsync(int id);
    Task<IEnumerable<PolicyResponseDto>> SearchPoliciesAsync(decimal minAmount, decimal maxAmount);
    Task<IEnumerable<PolicyResponseDto>> GetPoliciesByStatusAsync(bool isActive);
    Task<PolicyResponseDto> CreatePolicyAsync(PolicyCreateDto policyDto);
    Task<PolicyResponseDto?> UpdatePolicyAsync(int id, PolicyUpdateDto policyDto);
    Task<PolicyResponseDto?> UpdatePolicyStatusAsync(int id, bool isActive);
}