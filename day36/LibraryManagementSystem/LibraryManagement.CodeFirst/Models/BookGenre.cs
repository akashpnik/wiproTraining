using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.CodeFirst.Models
{
    public class BookGenre
    {
        public int BookID { get; set; }
        public Book Book { get; set; } = null!;

        public int GenreID { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}
