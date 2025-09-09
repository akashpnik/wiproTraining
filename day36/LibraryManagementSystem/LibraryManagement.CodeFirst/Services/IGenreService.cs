using LibraryManagement.CodeFirst.Models;

namespace LibraryManagement.CodeFirst.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre?> GetGenreByIdAsync(int id);
        Task<Genre> CreateGenreAsync(Genre genre);
        Task<Genre?> UpdateGenreAsync(int id, Genre genre);
        Task<bool> DeleteGenreAsync(int id);
    }
}
