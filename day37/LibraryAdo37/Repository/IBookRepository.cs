public interface IBookRepository : IGenericRepository<Book>
{
    Task<PagedResult<Book>> GetPageAsync(BookQuery query);
}
public interface IAuthorRepository : IGenericRepository<Author>
{
    Task<IReadOnlyList<Author>> GetAllAsync();
}
public interface IGenreRepository : IGenericRepository<Genre>
{
    Task<IReadOnlyList<Genre>> GetAllAsync();
}
