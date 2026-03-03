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
        var normalizedFilter = new PaginationFilter(
            validFilter.PageNumber,
            validFilter.PageSize
        );

        var response = new PagedResponse<List<T>>(
            pagedData, normalizedFilter.PageNumber, normalizedFilter.PageSize);

        var totalPages = (double)totalRecords / normalizedFilter.PageSize;
        var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        response.NextPage =
            normalizedFilter.PageNumber >= 1 && normalizedFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(
                    new PaginationFilter(
                        normalizedFilter.PageNumber + 1, normalizedFilter.PageSize), route)
                : null;
        response.PreviousPage =
            normalizedFilter.PageNumber > 1
                ? uriService.GetPageUri(
                    new PaginationFilter(
                        normalizedFilter.PageNumber - 1, normalizedFilter.PageSize), route)
                : null;

        response.FirstPage = roundedTotalPages > 0
            ? uriService.GetPageUri(new PaginationFilter(1, normalizedFilter.PageSize), route)
            : null;
        response.LastPage = roundedTotalPages > 0
            ? uriService.GetPageUri(
                new PaginationFilter(roundedTotalPages, normalizedFilter.PageSize), route)
            : null;
        response.TotalPages = roundedTotalPages;
        response.TotalRecords = totalRecords;

        return response;
    }
}
