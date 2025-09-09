using System.ComponentModel.DataAnnotations;

namespace ProfileBook.API.DTOs
{
    public class CreateReportDto
    {
        [Required]
        public int ReportedUserId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;
    }
}
