using capstone_policy_management.Data;
using capstone_policy_management.Entities;
using capstone_policy_management.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace capstone_policy_management.Repository.Implementations;

public class PolicyEnrollmentRepository : IPolicyEnrollmentRepository
{
    private readonly ApplicationDbContext dbContext;

    public PolicyEnrollmentRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<PolicyEnrollment>> GetAllEnrollmentsAsync()
    {
        return await dbContext.Set<PolicyEnrollment>()
            .Include(e => e.User)
            .Include(e => e.Policy)
            .ToListAsync();
    }

   
    public async Task<List<PolicyEnrollment>> GetEnrollmentsByStatusAsync(string status)
    {
        return await dbContext.Set<PolicyEnrollment>()
            .Include(e => e.User)
            .Include(e => e.Policy)
            .Where(e => e.Status == status)
            .ToListAsync();
    }

    public async Task<PolicyEnrollment?> GetEnrollmentByIdAsync(int id)
    {
        Console.WriteLine($"Fetching enrollment with ID: {id}");
        return await dbContext.Set<PolicyEnrollment>()
            .Include(e => e.User)
            .Include(e => e.Policy)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<PolicyEnrollment> CreateEnrollmentAsync(PolicyEnrollment enrollment)
    {
        dbContext.Set<PolicyEnrollment>().Add(enrollment);
        await dbContext.SaveChangesAsync();
        
        // Reload with navigation properties
        return (await GetEnrollmentByIdAsync(enrollment.Id))!;
    }

    public async Task<PolicyEnrollment> UpdateEnrollmentAsync(PolicyEnrollment enrollment)
    {
        dbContext.Set<PolicyEnrollment>().Update(enrollment);
        await dbContext.SaveChangesAsync();
        
        // Reload with navigation properties
        return (await GetEnrollmentByIdAsync(enrollment.Id))!;
    }

    
    public async Task<PolicyEnrollment?> GetEnrollmentByUserAndPolicyAsync(int userId, int policyId)
    {
        return await dbContext.Set<PolicyEnrollment>()
            .Include(e => e.User)
            .Include(e => e.Policy)
            .FirstOrDefaultAsync(e => e.UserId == userId && e.PolicyId == policyId);
    }

    public async Task DeleteEnrollmentAsync(int id)
    {
        var enrollment = await dbContext.Set<PolicyEnrollment>().FindAsync(id);
        if (enrollment != null)
        {
            dbContext.Set<PolicyEnrollment>().Remove(enrollment);
            await dbContext.SaveChangesAsync();
        }
    }

   
    public async Task<PolicyEnrollment?> ApproveEnrollmentAsync(int id)
    {
        var enrollment = await GetEnrollmentByIdAsync(id);
        if (enrollment == null) return null;

        enrollment.Status = "Approved";
        enrollment.ApprovedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        // Ensure RequestedAt is also UTC
        enrollment.RequestedAt = DateTime.SpecifyKind(enrollment.RequestedAt, DateTimeKind.Utc);

        return await UpdateEnrollmentAsync(enrollment);
    }

    
    public async Task<PolicyEnrollment?> RejectEnrollmentAsync(int id)
    {
        var enrollment = await GetEnrollmentByIdAsync(id);
        if (enrollment == null) return null;

        enrollment.Status = "Rejected";
        // Ensure RequestedAt is UTC
        enrollment.RequestedAt = DateTime.SpecifyKind(enrollment.RequestedAt, DateTimeKind.Utc);
        // ApprovedAt remains null for rejected enrollments

        return await UpdateEnrollmentAsync(enrollment);
    }

    public async Task<PolicyEnrollment?> GetEnrollmentByStatusAsync(int id, string status)
    {
        return await dbContext.Set<PolicyEnrollment>()
            .Include(e => e.User)
            .Include(e => e.Policy)
            .FirstOrDefaultAsync(e => e.Id == id && e.Status == status);
    }


}