using System.ComponentModel.DataAnnotations;

namespace ProfileBook.API.DTOs
{
    public class CreateCommentDto
    {
        [Required]
        [MaxLength(500)]
        public String CommentText { get; set; } = string.Empty;
    }
}
