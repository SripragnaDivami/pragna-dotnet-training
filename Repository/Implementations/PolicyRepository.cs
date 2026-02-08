
using capstone_policy_management.Entities;
using capstone_policy_management.Data;
using Microsoft.EntityFrameworkCore;
using capstone_policy_management.Repository.Interfaces;


namespace capstone_policy_management.Repository.Implwementations;

public class PolicyRepository : IPolicyRepository
{
   private readonly ApplicationDbContext dbContext;
    
    public PolicyRepository(ApplicationDbContext _dbContext)
    {
        dbContext = _dbContext;
      
    }
    public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
    {
        return await dbContext.Set<Policy>().ToListAsync();
       
    }

    public async Task<IEnumerable<Policy>> GetPoliciesByStatusAsync(bool isActive)
    {
        return await dbContext.Set<Policy>().Where(p => p.IsActive == isActive).ToListAsync();
    }

    public async Task<Policy?> GetPolicyByIdAsync(int id)
    {
        return await dbContext.Set<Policy>().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Policy>> SearchPoliciesByPremiumAmountAsync(decimal minAmount, decimal maxAmount)
    {
        return await dbContext.Set<Policy>().Where(p => p.PremiumAmount >= minAmount && p.PremiumAmount <= maxAmount).ToListAsync();
    }
    public async Task<Policy> CreatePolicyAsync(Policy policy)
    {
        dbContext.Set<Policy>().Add(policy);
        await dbContext.SaveChangesAsync();
        return policy;
    }
    public async Task<Policy> UpdatePolicyAsync(Policy policy)
    {
        dbContext.Set<Policy>().Update(policy);
        await dbContext.SaveChangesAsync();
        return policy;
    }
   
    public async Task<Policy?> UpdatePolicyStatusAsync(int id, bool isActive)
    {
        var policy = await GetPolicyByIdAsync(id);
        if (policy == null) return null;

        policy.IsActive = isActive;
        policy.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        policy.CreatedAt = DateTime.SpecifyKind(policy.CreatedAt, DateTimeKind.Utc);

        dbContext.Set<Policy>().Update(policy);
        await dbContext.SaveChangesAsync();
        return policy;
    }
}