using Microsoft.AspNetCore.Http;
using Shared.Core.Configurations.Common.Sessions;


namespace Shared.Core.Configurations.Common.Middleware;
public class UserSessionMiddleware
{
    private readonly RequestDelegate _next;

    public UserSessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            Session.SetUser(context.User);
        }
        else
        {
            Session.Clear();
        }

        await _next(context);
    }
}
