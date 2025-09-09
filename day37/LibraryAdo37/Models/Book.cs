using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id { get; set; }

    [Required, MaxLength(160)]
    public string Title { get; set; } = "";

    [Range(1,int.MaxValue)]
    public int AuthorId { get; set; }

    [Range(1,int.MaxValue)]
    public int GenreId { get; set; }

    [Range(0,double.MaxValue)]
    public decimal Price { get; set; }

    [DataType(DataType.Date)]
    public DateTime? PublishedOn { get; set; }

    // read-only helpers for list screens
    public string? AuthorName { get; set; }
    public string? GenreName { get; set; }
}
