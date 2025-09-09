using System.ComponentModel.DataAnnotations;

namespace ProfileBook.API.DTOs
{
    public class CreateMessageDto
    {
        [Required]
        public int ReceiverId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string MessageContent { get; set; } = string.Empty;
    }
}
