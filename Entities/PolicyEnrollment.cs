using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_policy_management.Entities;


[Table("policy_enrollments")]
public class PolicyEnrollment
{
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    [Required(ErrorMessage = "User ID is required.")]
    public int UserId { get; set; }

    [Column("policy_id")]
    [Required(ErrorMessage = "Policy ID is required.")]
    public int PolicyId { get; set; }

    [Column("status")]
    [Required(ErrorMessage = "Status is required.")]
    [StringLength(10)]
    public string Status { get; set; } = "Pending";

    [Column("requested_at")]
    [Required(ErrorMessage = "Requested date is required.")]
    public DateTime RequestedAt { get; set; }

    [Column("approved_at")]
    public DateTime? ApprovedAt { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [ForeignKey("PolicyId")]
    public Policy Policy { get; set; } = null!;
}