using Todo.WebApi.Filters;
using Todo.WebApi.Response;
using Todo.WebApi.Response.Pagination;

namespace Todo.WebApi.Helper;

/// <summary>
/// PaginationHelper
/// </summary>
public static class PaginationHelper
{
    /// <summary>
    /// CreatePagedResponse
    /// </summary>
    /// <param name="pagedData"></param>
    /// <param name="validFilter"></param>
    /// <param name="totalRecords"></param>
    /// <param name="uriService"></param>
    /// <param name="route"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static PagedResponse<List<T>> CreatePagedResponse<T>(
        List<T> pagedData,
        PaginationFilter validFilter,
        int totalRecords,
        IUriService uriService,
        string route)
    {
        // construct a PagedResponse
        var response = new PagedResponse<List<T>>(
            pagedData, validFilter.PageNumber, validFilter.PageSize);

        var totalPages = (double)totalRecords / validFilter.PageSize;
        var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        response.NextPage =
            validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(
                    new PaginationFilter(
                        validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
        response.PreviousPage =
            validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(
                    new PaginationFilter(
                        validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;

        response.FirstPage = uriService.GetPageUri(
            new PaginationFilter(1, validFilter.PageSize), route);
        response.LastPage = uriService.GetPageUri(
            new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
        response.TotalPages = roundedTotalPages;
        response.TotalRecords = totalRecords;

        return response;
    }
}
