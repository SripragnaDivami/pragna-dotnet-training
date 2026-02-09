using capstone_policy_management.Entities;

namespace capstone_policy_management.Repository.Interfaces
{
   
    public interface IPolicyEnrollmentRepository
    {
        Task<List<PolicyEnrollment>> GetAllEnrollmentsAsync();
        Task<PolicyEnrollment?> GetEnrollmentByIdAsync(int id);
        Task<PolicyEnrollment> CreateEnrollmentAsync(PolicyEnrollment enrollment);
        Task<PolicyEnrollment> UpdateEnrollmentAsync(PolicyEnrollment enrollment);
        Task<PolicyEnrollment?> ApproveEnrollmentAsync(int id);
        Task<PolicyEnrollment?> RejectEnrollmentAsync(int id);
        Task<List<PolicyEnrollment>> GetEnrollmentsByStatusAsync(string status);
        Task<PolicyEnrollment?> GetEnrollmentByUserAndPolicyAsync(int userId, int policyId);
        
        Task DeleteEnrollmentAsync(int id);
    }
}