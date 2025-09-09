using Microsoft.EntityFrameworkCore;
using LibraryManagement.CodeFirst.Models;

namespace LibraryManagement.CodeFirst.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Author entity
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.AuthorID);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Bio).HasMaxLength(500);
            });

            // Configure Book entity
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.BookID);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.ISBN).HasMaxLength(13);
                
                // Configure one-to-many relationship
                entity.HasOne(b => b.Author)
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Genre entity
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.GenreID);
                entity.Property(g => g.Name).IsRequired().HasMaxLength(50);
                entity.Property(g => g.Description).HasMaxLength(200);
            });

            // Configure many-to-many relationship between Book and Genre
            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.HasKey(bg => new { bg.BookID, bg.GenreID });

                entity.HasOne(bg => bg.Book)
                      .WithMany(b => b.BookGenres)
                      .HasForeignKey(bg => bg.BookID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(bg => bg.Genre)
                      .WithMany(g => g.BookGenres)
                      .HasForeignKey(bg => bg.GenreID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed data
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorID = 1, Name = "J.K. Rowling", Bio = "British author, best known for the Harry Potter series" },
                new Author { AuthorID = 2, Name = "Stephen King", Bio = "American author of horror, supernatural fiction, suspense, crime, science-fiction, and fantasy novels" }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreID = 1, Name = "Fantasy", Description = "Fantasy fiction" },
                new Genre { GenreID = 2, Name = "Horror", Description = "Horror fiction" },
                new Genre { GenreID = 3, Name = "Mystery", Description = "Mystery and thriller" }
            );
        }
    }
}
