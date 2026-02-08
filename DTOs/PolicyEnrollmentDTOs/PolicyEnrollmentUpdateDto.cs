using System.ComponentModel.DataAnnotations;

namespace capstone_policy_management.DTOs.PolicyEnrollmentDTOs
{
    public class PolicyEnrollmentUpdateDto
    {
        [StringLength(10)]
        public string? Status { get; set; }
        
        public DateTime? ApprovedAt { get; set; }
    }
}
