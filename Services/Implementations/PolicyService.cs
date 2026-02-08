using capstone_policy_management.DTOs;
using capstone_policy_management.DTOs.PolicyDTOs;
using capstone_policy_management.Entities;
using capstone_policy_management.Repository.Interfaces;
using capstone_policy_management.Services.Interfaces;

namespace capstone_policy_management.Services.Implementations;


public class PolicyService : IPolicyService
{
    private readonly IPolicyRepository policyRepository;
    public PolicyService(IPolicyRepository _policyRepository)
    {
        this.policyRepository = _policyRepository;
    }

    public async Task<IEnumerable<PolicyResponseDto>> GetPoliciesAsync()
    {
        var policies = await policyRepository.GetAllPoliciesAsync();
        return policies.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<PolicyResponseDto>> GetPoliciesByStatusAsync(bool isActive)
    {
        var policies = await policyRepository.GetPoliciesByStatusAsync(isActive);
        return policies.Select(MapToResponseDto);
    }

    public async Task<PolicyResponseDto?> GetPolicyByIdAsync(int id)
    {
        var policy = await policyRepository.GetPolicyByIdAsync(id);
        return policy != null ? MapToResponseDto(policy) : null;
    }

    public async Task<IEnumerable<PolicyResponseDto>> SearchPoliciesAsync(decimal minAmount, decimal maxAmount)
    {
        var policies = await policyRepository.SearchPoliciesByPremiumAmountAsync(minAmount, maxAmount);
        return policies.Select(MapToResponseDto);
    }

    public async Task<PolicyResponseDto> CreatePolicyAsync(PolicyCreateDto policyDto)
    {
        
        var policy = new Policy
        {
            PolicyCode = policyDto.PolicyCode,
            Name = policyDto.Name,
            PremiumAmount = policyDto.PremiumAmount,
            Description = policyDto.Description,
            IsActive = policyDto.IsActive,
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
        };

        var createdPolicy = await policyRepository.CreatePolicyAsync(policy);
        return MapToResponseDto(createdPolicy);
    }

    public async Task<PolicyResponseDto?> UpdatePolicyAsync(int id, PolicyUpdateDto policyDto)
    {
        
        var existingPolicy = await policyRepository.GetPolicyByIdAsync(id);
        if (existingPolicy == null)
            return null;

        
        if (!string.IsNullOrEmpty(policyDto.PolicyCode))
            existingPolicy.PolicyCode = policyDto.PolicyCode;

        if (!string.IsNullOrEmpty(policyDto.Name))
            existingPolicy.Name = policyDto.Name;

        if (policyDto.PremiumAmount.HasValue)
            existingPolicy.PremiumAmount = policyDto.PremiumAmount.Value;

        if (policyDto.Description != null)
            existingPolicy.Description = policyDto.Description;

        if (policyDto.IsActive.HasValue)
            existingPolicy.IsActive = policyDto.IsActive.Value;

        existingPolicy.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        existingPolicy.CreatedAt = DateTime.SpecifyKind(existingPolicy.CreatedAt, DateTimeKind.Utc);

        var updatedPolicy = await policyRepository.UpdatePolicyAsync(existingPolicy);
        return MapToResponseDto(updatedPolicy);
    }

    private static PolicyResponseDto MapToResponseDto(Policy policy)
    {
        return new PolicyResponseDto
        {
            Id = policy.Id,
            PolicyCode = policy.PolicyCode,
            Name = policy.Name,
            PremiumAmount = policy.PremiumAmount,
            Description = policy.Description,
            IsActive = policy.IsActive,
            CreatedAt = policy.CreatedAt,
            UpdatedAt = policy.UpdatedAt
        };
    }


    public async Task<PolicyResponseDto?> UpdatePolicyStatusAsync(int id, bool isActive)
    {
        var policy = await policyRepository.UpdatePolicyStatusAsync(id, isActive);
        return policy != null ? MapToResponseDto(policy) : null;
    }
}