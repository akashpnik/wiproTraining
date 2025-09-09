public sealed class BookQuery
{
    public string? Search { get; set; }
    public string? SortBy { get; set; } = "Title"; // Title|Author|Genre|Price|PublishedOn
    public bool Desc { get; set; } = false;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
