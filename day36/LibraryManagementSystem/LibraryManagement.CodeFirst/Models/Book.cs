using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.CodeFirst.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(13)]
        public string ISBN { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; }

        // Foreign key
        public int AuthorID { get; set; }

        // Navigation property
        [ForeignKey("AuthorID")]
        public Author Author { get; set; } = null!;

        // Navigation property for many-to-many relationship with genres
        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
