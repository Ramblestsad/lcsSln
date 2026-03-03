namespace Todo.WebApi.Filters;

/// <summary>
/// Pagination filter
/// </summary>
public class PaginationFilter
{
    private const int DefaultPageNumber = 1;
    private const int DefaultPageSize = 10;
    private const int MaxPageSize = 10;
    private int _pageNumber = DefaultPageNumber;
    private int _pageSize = DefaultPageSize;

    /// <summary>
    /// Page number
    /// </summary>
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? DefaultPageNumber : value;
    }

    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value switch
        {
            < 1 => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            _ => value,
        };
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public PaginationFilter()
    {
        PageNumber = DefaultPageNumber;
        PageSize = DefaultPageSize;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    public PaginationFilter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
