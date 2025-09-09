using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.CodeFirst.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        // Navigation property for many-to-many relationship
        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
