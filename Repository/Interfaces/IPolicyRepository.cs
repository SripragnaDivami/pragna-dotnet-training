namespace capstone_policy_management.Repository.Interfaces;
using capstone_policy_management.Entities;


public interface IPolicyRepository
{
    Task<IEnumerable<Policy>> GetAllPoliciesAsync();
    Task<Policy?> GetPolicyByIdAsync(int id);
    Task<IEnumerable<Policy>> SearchPoliciesByPremiumAmountAsync(decimal minAmount, decimal maxAmount);
    Task<IEnumerable<Policy>> GetPoliciesByStatusAsync(bool isActive);
    Task<Policy> CreatePolicyAsync(Policy policy);
    Task<Policy> UpdatePolicyAsync(Policy policy);
    Task<Policy?> UpdatePolicyStatusAsync(int id, bool isActive);
}
