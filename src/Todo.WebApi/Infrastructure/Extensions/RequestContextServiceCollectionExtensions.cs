using System.Security.Claims;
using Todo.WebApi.Infrastructure.RequestContext;
using Todo.WebApi.Todos.Contracts;

namespace Todo.WebApi.Infrastructure.Extensions;

public static class RequestContextServiceCollectionExtensions
{
    public static IServiceCollection AddTodoRequestContext(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IUriService, UriService>();

        services.AddScoped<CurrentUser>(provider =>
        {
            var accessor = provider.GetRequiredService<IHttpContextAccessor>();
            var context = accessor.HttpContext;

            var currentUser = new CurrentUser();
            var user = context?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                currentUser.IsAuthenticated = true;
                currentUser.Subject = user.FindFirst("sub")?.Value
                                      ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                currentUser.Name = user.Identity.Name;
                currentUser.Roles = user.FindAll(ClaimTypes.Role)
                    .Select(claim => claim.Value)
                    .Distinct()
                    .ToArray();
            }

            return currentUser;
        });

        return services;
    }
}
