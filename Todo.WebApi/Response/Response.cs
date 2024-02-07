namespace Todo.WebApi.Response;

public class Response<T>
{
    protected Response() { }
    public Response(T data)
    {
        Succeeded = true;
        StatusCode = StatusCodes.Status200OK;
        Message = string.Empty;
        Errors = null;
        Data = data;
    }

    public T? Data { get; set; }
    public bool Succeeded { get; set; }
    public int StatusCode { get; set; }
    public string[]? Errors { get; set; }
    public string? Message { get; set; }
}

public class PagedResponse<T> : Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Uri? FirstPage { get; set; }
    public Uri? LastPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public Uri? NextPage { get; set; }
    public Uri? PreviousPage { get; set; }

    public PagedResponse(T data, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
        Message = null;
        Succeeded = true;
        Errors = null;
    }
}
