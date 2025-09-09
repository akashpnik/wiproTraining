using Microsoft.Data.SqlClient;
using System.Reflection;

public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
{
    private readonly ISqlConnectionFactory _factory;
    private readonly string _table;
    private readonly PropertyInfo[] _props;

    public GenericRepository(ISqlConnectionFactory factory, string tableName)
    {
        _factory = factory;
        _table = tableName;
        _props = typeof(T).GetProperties();
    }

    public async Task<int> CreateAsync(T entity)
    {
        var cols = string.Join(",", _props.Where(p => p.Name != "Id").Select(p => p.Name));
        var parms = string.Join(",", _props.Where(p => p.Name != "Id").Select(p => "@" + p.Name));

        var sql = $"INSERT INTO {_table} ({cols}) VALUES ({parms}); SELECT SCOPE_IDENTITY();";

        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);

        foreach (var p in _props.Where(p => p.Name != "Id"))
            cmd.Parameters.AddWithValue("@" + p.Name, p.GetValue(entity) ?? DBNull.Value);

        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM {_table} WHERE Id=@Id";
        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var r = await cmd.ExecuteReaderAsync();
        if (!await r.ReadAsync()) return null;

        return Map(r);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var setClause = string.Join(",", _props.Where(p => p.Name != "Id")
            .Select(p => $"{p.Name}=@{p.Name}"));
        var sql = $"UPDATE {_table} SET {setClause} WHERE Id=@Id";

        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);

        foreach (var p in _props)
            cmd.Parameters.AddWithValue("@" + p.Name, p.GetValue(entity) ?? DBNull.Value);

        return await cmd.ExecuteNonQueryAsync() == 1;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sql = $"DELETE FROM {_table} WHERE Id=@Id";
        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        return await cmd.ExecuteNonQueryAsync() == 1;
    }

    private static T Map(SqlDataReader r)
    {
        var obj = new T();
        foreach (var p in typeof(T).GetProperties())
        {
            if (!HasColumn(r, p.Name) || r[p.Name] is DBNull) continue;
            p.SetValue(obj, r[p.Name]);
        }
        return obj;
    }

    private static bool HasColumn(SqlDataReader reader, string col)
    {
        for (int i = 0; i < reader.FieldCount; i++)
            if (reader.GetName(i).Equals(col, StringComparison.OrdinalIgnoreCase))
                return true;
        return false;
    }
}

