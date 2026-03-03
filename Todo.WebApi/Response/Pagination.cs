using Microsoft.AspNetCore.WebUtilities;
using Todo.WebApi.Filters;

namespace Todo.WebApi.Response.Pagination;

public interface IUriService
{
    public Uri GetPageUri(PaginationFilter filter, string route);
}

public class UriService: IUriService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UriService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Uri GetPageUri(PaginationFilter filter, string route)
    {
        var request = _httpContextAccessor.HttpContext?.Request
                      ?? throw new InvalidOperationException(
                          "An active HTTP request is required to generate pagination links.");

        var endpointUri = new Uri(
            $"{request.Scheme}://{request.Host.ToUriComponent()}{route}");
        var modifiedUri = QueryHelpers.AddQueryString(
            endpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
        modifiedUri = QueryHelpers.AddQueryString(
            modifiedUri, "pageSize", filter.PageSize.ToString());
        return new Uri(modifiedUri);
    }
}
