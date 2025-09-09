using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagement.DatabaseFirst.Models;

[Index("AuthorId", Name = "IX_Books_AuthorID")]
public partial class Book
{
    [Key]
    [Column("BookID")]
    public int BookId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    [Column("ISBN")]
    [StringLength(13)]
    public string Isbn { get; set; } = null!;

    public DateTime PublishedDate { get; set; }

    [Column("AuthorID")]
    public int AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Books")]
    public virtual Author Author { get; set; } = null!;

    [ForeignKey("BookId")]
    [InverseProperty("Books")]
    
    
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
