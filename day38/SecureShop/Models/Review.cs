using System.ComponentModel.DataAnnotations;

namespace SecureShop.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required, StringLength(1000)]
        public string Content { get; set; } = string.Empty; // Razor auto encodes

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(100)]
        public string? AuthorName { get; set; }
    }
}
