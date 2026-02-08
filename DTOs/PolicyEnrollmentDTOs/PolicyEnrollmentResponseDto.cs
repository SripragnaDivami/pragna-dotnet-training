namespace capstone_policy_management.DTOs.PolicyEnrollmentDTOs
{
  
    public class PolicyEnrollmentResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public int PolicyId { get; set; }
        public string PolicyCode { get; set; } = string.Empty;
        public string PolicyName { get; set; } = string.Empty;
        public decimal PremiumAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime RequestedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
