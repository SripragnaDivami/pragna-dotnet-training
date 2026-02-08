using System.ComponentModel.DataAnnotations;

namespace capstone_policy_management.DTOs.UserDTOs
{
    public class UserUpdateDto
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string? Password { get; set; }

        public string? Role { get; set; }
    }
}
