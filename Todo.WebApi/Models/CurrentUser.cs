namespace Todo.WebApi.Models;

public sealed class CurrentUser
{
    public bool IsAuthenticated { get; set; }
    public string? Subject { get; set; } // user id / sub
    public string? Name { get; set; }
    public string[] Roles { get; set; } = [];
}
