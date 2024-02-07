namespace Todo.WebApi.Filters;
/// <summary>
/// Pagination filter
/// </summary>
public class PaginationFilter
{
    /// <summary>
    /// Page number
    /// </summary>
    public int PageNumber { get; set; }
    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public PaginationFilter()
    {
        PageNumber = 1;
        PageSize = 10;
    }
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    public PaginationFilter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 10 ? 10 : pageSize;
    }
}
