using Microsoft.Data.SqlClient;

public sealed class AuthorRepository : IAuthorRepository
{
    private readonly ISqlConnectionFactory _f;
    public AuthorRepository(ISqlConnectionFactory f) => _f = f;

    public async Task<int> CreateAsync(Author a)
    {
        const string sql = "INSERT INTO Authors(Name) VALUES(@Name); SELECT SCOPE_IDENTITY();";
        using var c = _f.Create(); await c.OpenAsync();
        using var cmd = new SqlCommand(sql, c);
        cmd.Parameters.AddWithValue("@Name", a.Name);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task<Author?> GetByIdAsync(int id)
    {
        using var c = _f.Create(); await c.OpenAsync();
        using var cmd = new SqlCommand("SELECT Id,Name FROM Authors WHERE Id=@Id", c);
        cmd.Parameters.AddWithValue("@Id", id);
        using var r = await cmd.ExecuteReaderAsync();
        return await r.ReadAsync() ? new Author { Id=r.GetInt32(0), Name=r.GetString(1) } : null;
    }
    public async Task<bool> UpdateAsync(Author a)
    {
        using var c = _f.Create(); await c.OpenAsync();
        using var cmd = new SqlCommand("UPDATE Authors SET Name=@Name WHERE Id=@Id", c);
        cmd.Parameters.AddWithValue("@Name", a.Name);
        cmd.Parameters.AddWithValue("@Id", a.Id);
        return await cmd.ExecuteNonQueryAsync() == 1;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        using var c = _f.Create(); await c.OpenAsync();
        using var cmd = new SqlCommand("DELETE FROM Authors WHERE Id=@Id", c);
        cmd.Parameters.AddWithValue("@Id", id);
        return await cmd.ExecuteNonQueryAsync() == 1;
    }
    public async Task<IReadOnlyList<Author>> GetAllAsync()
    {
        var list = new List<Author>();
        using var c = _f.Create(); await c.OpenAsync();
        using var cmd = new SqlCommand("SELECT Id,Name FROM Authors ORDER BY Name", c);
        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync()) list.Add(new Author { Id=r.GetInt32(0), Name=r.GetString(1)});
        return list;
    }
}
