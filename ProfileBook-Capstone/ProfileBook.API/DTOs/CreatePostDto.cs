using System.ComponentModel.DataAnnotations;

namespace ProfileBook.API.DTOs
{
    public class CreatePostDto
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; } = string.Empty;

        public IFormFile? PostImage { get; set; }
    }
}
