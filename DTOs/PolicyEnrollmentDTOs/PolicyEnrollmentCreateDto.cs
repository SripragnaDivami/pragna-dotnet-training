using System.ComponentModel.DataAnnotations;

namespace capstone_policy_management.DTOs.PolicyEnrollmentDTOs
{

    public class PolicyEnrollmentCreateDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Policy ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Policy ID must be a positive number.")]
        public int PolicyId { get; set; }
    }
}
