using CSHM.Api.Extensions;

namespace CSHM.Api.Middlewares;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorLoggingMiddleware(RequestDelegate next)
    {
        _next = next;

    }

    public async Task Invoke(HttpContext context/*, ILogWidget log*/)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var userId = context.User.GetUserID();
            //og.ExceptionLog(e, "ErrorLoggingMiddleware", userId, ip);

            //System.Diagnostics.Debug.WriteLine($"The following error happened: {e.Message}");

            throw;
        }
    }
}