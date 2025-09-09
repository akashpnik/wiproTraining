/*using System.ComponentModel.DataAnnotations;

namespace ProfileBook.API.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User";

        public string? ProfileImage { get; set; }
        
        // Optional password change
        public string? NewPassword { get; set; }
    }
}
*/