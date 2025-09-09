using System.ComponentModel.DataAnnotations;

namespace SecureShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = string.Empty;

        [Required, Range(0.01, 100000)]
        public decimal Price { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public List<Review> Reviews { get; set; } = [];
    }
}
