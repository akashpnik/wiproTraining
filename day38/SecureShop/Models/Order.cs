using System.ComponentModel.DataAnnotations;

namespace SecureShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public List<OrderItem> Items { get; set; } = [];
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);
    }
}
