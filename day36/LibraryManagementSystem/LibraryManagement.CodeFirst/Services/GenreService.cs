using LibraryManagement.CodeFirst.Data;
using LibraryManagement.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.CodeFirst.Services
{
    public class GenreService : IGenreService
    {
        private readonly LibraryDbContext _context;

        public GenreService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            try
            {
                return await _context.Genres.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving genres: {ex.Message}", ex);
            }
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            try
            {
                return await _context.Genres
                    .Include(g => g.BookGenres)
                        .ThenInclude(bg => bg.Book)
                    .FirstOrDefaultAsync(g => g.GenreID == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving genre with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<Genre> CreateGenreAsync(Genre genre)
        {
            try
            {
                _context.Genres.Add(genre);
                await _context.SaveChangesAsync();
                return genre;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating genre: {ex.Message}", ex);
            }
        }

        public async Task<Genre?> UpdateGenreAsync(int id, Genre genre)
        {
            try
            {
                var existingGenre = await _context.Genres.FindAsync(id);
                if (existingGenre == null)
                    return null;

                existingGenre.Name = genre.Name;
                existingGenre.Description = genre.Description;

                await _context.SaveChangesAsync();
                return existingGenre;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating genre: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            try
            {
                var genre = await _context.Genres.FindAsync(id);
                if (genre == null)
                    return false;

                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting genre: {ex.Message}", ex);
            }
        }
    }
}
