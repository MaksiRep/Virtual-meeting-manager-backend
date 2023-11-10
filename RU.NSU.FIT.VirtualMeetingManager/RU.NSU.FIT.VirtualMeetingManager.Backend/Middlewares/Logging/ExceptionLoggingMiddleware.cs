using Serilog;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Middlewares.Logging;

public class ExceptionLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            Log.Error("Error during HTTP {RequestMethod} {RequestPath} request: {Error}\n{StackTrace}",
                context.Request.Method, context.Request.Path, e.Message, e.StackTrace);
            throw;
        }
    }
}