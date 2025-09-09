using LibraryManagement.CodeFirst.Data;
using LibraryManagement.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.CodeFirst.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext _context;

        public AuthorService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            try
            {
                return await _context.Authors
                    .Include(a => a.Books)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving authors: {ex.Message}", ex);
            }
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            try
            {
                return await _context.Authors
                    .Include(a => a.Books)
                    .FirstOrDefaultAsync(a => a.AuthorID == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving author with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            try
            {
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return author;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating author: {ex.Message}", ex);
            }
        }

        public async Task<Author?> UpdateAuthorAsync(int id, Author author)
        {
            try
            {
                var existingAuthor = await _context.Authors.FindAsync(id);
                if (existingAuthor == null)
                    return null;

                existingAuthor.Name = author.Name;
                existingAuthor.Bio = author.Bio;

                await _context.SaveChangesAsync();
                return existingAuthor;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating author: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                    return false;

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting author: {ex.Message}", ex);
            }
        }
    }
}
