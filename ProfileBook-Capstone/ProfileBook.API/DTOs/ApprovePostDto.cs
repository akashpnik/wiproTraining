using System.ComponentModel.DataAnnotations;

namespace ProfileBook.API.DTOs
{
    public class ApprovePostDto
    {
        [Required]
        public int PostId { get; set; }
        
        [Required]
        public string Action { get; set; } = string.Empty; // "Approve" or "Reject"
        
        public string? Comments { get; set; }
    }
}
