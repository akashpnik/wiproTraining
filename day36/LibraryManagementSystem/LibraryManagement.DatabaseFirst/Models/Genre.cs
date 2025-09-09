using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DatabaseFirst.Models;

public partial class Genre
{
    [Key]
    [Column("GenreID")]
    public int GenreId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    public string Description { get; set; } = null!;

    [ForeignKey("GenreId")]
    [InverseProperty("Genres")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
