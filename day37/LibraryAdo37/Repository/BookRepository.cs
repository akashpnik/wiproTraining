using Microsoft.Data.SqlClient;
using System.Text;

public sealed class BookRepository : IBookRepository
{
    private readonly ISqlConnectionFactory _factory;
    private static readonly HashSet<string> AllowedSort = new(StringComparer.OrdinalIgnoreCase)
        { "Title","Author","Genre","Price","PublishedOn" };

    public BookRepository(ISqlConnectionFactory factory) => _factory = factory;

    public async Task<int> CreateAsync(Book b)
    {
        const string sql = @"
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublishedOn)
VALUES (@Title,@AuthorId,@GenreId,@Price,@PublishedOn);
SELECT SCOPE_IDENTITY();";
        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Title", b.Title);
        cmd.Parameters.AddWithValue("@AuthorId", b.AuthorId);
        cmd.Parameters.AddWithValue("@GenreId", b.GenreId);
        cmd.Parameters.AddWithValue("@Price", b.Price);
        cmd.Parameters.AddWithValue("@PublishedOn", (object?)b.PublishedOn ?? DBNull.Value);
        var id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
        return id;
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        const string sql = @"
SELECT b.Id, b.Title, b.AuthorId, b.GenreId, b.Price, b.PublishedOn,
       a.Name AS AuthorName, g.Name AS GenreName
FROM Books b
JOIN Authors a ON a.Id=b.AuthorId
JOIN Genres  g ON g.Id=b.GenreId
WHERE b.Id=@Id;";
        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        using var r = await cmd.ExecuteReaderAsync();
        if (!await r.ReadAsync()) return null;
        return Map(r);
    }

    public async Task<bool> UpdateAsync(Book b)
    {
        const string sql = @"
UPDATE Books SET Title=@Title, AuthorId=@AuthorId, GenreId=@GenreId,
                 Price=@Price, PublishedOn=@PublishedOn
WHERE Id=@Id;";
        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Title", b.Title);
        cmd.Parameters.AddWithValue("@AuthorId", b.AuthorId);
        cmd.Parameters.AddWithValue("@GenreId", b.GenreId);
        cmd.Parameters.AddWithValue("@Price", b.Price);
        cmd.Parameters.AddWithValue("@PublishedOn", (object?)b.PublishedOn ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Id", b.Id);
        return await cmd.ExecuteNonQueryAsync() == 1;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Books WHERE Id=@Id;";
        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        return await cmd.ExecuteNonQueryAsync() == 1;
    }

    public async Task<PagedResult<Book>> GetPageAsync(BookQuery q)
    {
        var sort = AllowedSort.Contains(q.SortBy ?? "") ? q.SortBy! : "Title";
        var order = q.Desc ? "DESC" : "ASC";
        var where = new StringBuilder("WHERE 1=1 ");
        if (!string.IsNullOrWhiteSpace(q.Search))
            where.Append("AND (b.Title LIKE @S OR a.Name LIKE @S OR g.Name LIKE @S) ");

        // Count first (for total)
        var countSql = $@"SELECT COUNT(*) 
FROM Books b
JOIN Authors a ON a.Id=b.AuthorId
JOIN Genres  g ON g.Id=b.GenreId
{where};";

        // Page query with OFFSET/FETCH (optimized, indexed columns recommended)
        var pageSql = $@"
SELECT b.Id, b.Title, b.AuthorId, b.GenreId, b.Price, b.PublishedOn,
       a.Name AS AuthorName, g.Name AS GenreName
FROM Books b
JOIN Authors a ON a.Id=b.AuthorId
JOIN Genres  g ON g.Id=b.GenreId
{where}
ORDER BY {(sort switch {
    "Author" => "a.Name",
    "Genre"  => "g.Name",
    "Price"  => "b.Price",
    "PublishedOn" => "b.PublishedOn",
    _ => "b.Title"
})} {order}
OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

        using var conn = _factory.Create();
        await conn.OpenAsync();

        int total;
        using (var countCmd = new SqlCommand(countSql, conn))
        {
            if (!string.IsNullOrWhiteSpace(q.Search))
                countCmd.Parameters.AddWithValue("@S", $"%{q.Search}%");
            total = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
        }

        var list = new List<Book>();
        using (var pageCmd = new SqlCommand(pageSql, conn))
        {
            if (!string.IsNullOrWhiteSpace(q.Search))
                pageCmd.Parameters.AddWithValue("@S", $"%{q.Search}%");
            var skip = (q.Page - 1) * q.PageSize;
            pageCmd.Parameters.AddWithValue("@Skip", skip);
            pageCmd.Parameters.AddWithValue("@Take", q.PageSize);

            using var r = await pageCmd.ExecuteReaderAsync();
            while (await r.ReadAsync()) list.Add(Map(r));
        }

        return new PagedResult<Book> { Items = list, Total = total, Page = q.Page, PageSize = q.PageSize };
    }

    private static Book Map(SqlDataReader r) => new()
    {
        Id = r.GetInt32(r.GetOrdinal("Id")),
        Title = r.GetString(r.GetOrdinal("Title")),
        AuthorId = r.GetInt32(r.GetOrdinal("AuthorId")),
        GenreId  = r.GetInt32(r.GetOrdinal("GenreId")),
        Price = r.GetDecimal(r.GetOrdinal("Price")),
        PublishedOn = r.IsDBNull(r.GetOrdinal("PublishedOn")) ? null : r.GetDateTime(r.GetOrdinal("PublishedOn")),
        AuthorName = r.GetString(r.GetOrdinal("AuthorName")),
        GenreName  = r.GetString(r.GetOrdinal("GenreName"))
    };
}
