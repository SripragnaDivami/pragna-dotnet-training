namespace capstone_policy_management.DTOs.PolicyDTOs
{

    public class PolicyResponseDto
    {
        public int Id { get; set; }
        public string PolicyCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal PremiumAmount { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
