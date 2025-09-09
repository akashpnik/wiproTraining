using LibraryManagement.DatabaseFirst.Models;

namespace LibraryManagement.DatabaseFirst.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book book);
        Task<Book?> UpdateBookAsync(int id, Book book);
        Task<bool> DeleteBookAsync(int id);
        Task<IEnumerable<Book>> GetBooksWithAuthorsAndGenresAsync();
    }
}
