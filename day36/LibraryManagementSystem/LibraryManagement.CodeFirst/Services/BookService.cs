using LibraryManagement.CodeFirst.Data;
using LibraryManagement.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.CodeFirst.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            try
            {
                return await _context.Books
                    .Include(b => b.Author)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving books: {ex.Message}", ex);
            }
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            try
            {
                return await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.BookGenres)
                        .ThenInclude(bg => bg.Genre)
                    .FirstOrDefaultAsync(b => b.BookID == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving book with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating book: {ex.Message}", ex);
            }
        }

        public async Task<Book?> UpdateBookAsync(int id, Book book)
        {
            try
            {
                var existingBook = await _context.Books.FindAsync(id);
                if (existingBook == null)
                    return null;

                existingBook.Title = book.Title;
                existingBook.ISBN = book.ISBN;
                existingBook.PublishedDate = book.PublishedDate;
                existingBook.AuthorID = book.AuthorID;

                await _context.SaveChangesAsync();
                return existingBook;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating book: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                    return false;

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting book: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthorsAndGenresAsync()
        {
            try
            {
                return await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.BookGenres)
                        .ThenInclude(bg => bg.Genre)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving books with authors and genres: {ex.Message}", ex);
            }
        }
    }
}
