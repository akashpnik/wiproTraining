using System.ComponentModel.DataAnnotations;

public class Genre
{
    public int Id { get; set; }
    [Required, MaxLength(80)] public string Name { get; set; } = "";
}
