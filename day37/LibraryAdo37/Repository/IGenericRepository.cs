public interface IGenericRepository<T>
{
    Task<int> CreateAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}
