using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.CodeFirst.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Bio { get; set; } = string.Empty;

        // Navigation property for one-to-many relationship
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
