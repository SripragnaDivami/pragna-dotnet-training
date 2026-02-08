using System.ComponentModel.DataAnnotations;

namespace capstone_policy_management.DTOs.PolicyDTOs
{
    public class PolicyCreateDto
    {
        [Required(ErrorMessage = "Policy code is required.")]
        [StringLength(50, ErrorMessage = "Policy code cannot exceed 50 characters.")]
        public string PolicyCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Policy name is required.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Policy name must be between 3 and 150 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Premium amount is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Premium amount must be greater than 0.")]
        public decimal PremiumAmount { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
