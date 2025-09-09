using System.ComponentModel.DataAnnotations;

public class Author
{
    public int Id { get; set; }
    [Required, MaxLength(120)] public string Name { get; set; } = "";
}

