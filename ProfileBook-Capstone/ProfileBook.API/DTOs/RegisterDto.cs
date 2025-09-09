
using System.ComponentModel.DataAnnotations;

namespace ProfileBook.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        //[StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress] //       [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // Default to User, can be Admin
    }
}
