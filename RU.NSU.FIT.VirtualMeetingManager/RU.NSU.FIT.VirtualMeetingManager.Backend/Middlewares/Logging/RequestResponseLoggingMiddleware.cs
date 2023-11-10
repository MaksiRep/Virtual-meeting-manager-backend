using System.Diagnostics;
using System.Net;
using Serilog;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Middlewares.Logging;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = new Stopwatch();

        watch.Start();
        Log.Information("Started HTTP {RequestMethod} {RequestPath} request at {StartDateTime:dd.MM.yyyy HH:mm:ssZ}\nHeaders:{RequestHeaders}",
            context.Request.Method, context.Request.Path, DateTime.UtcNow, context.Request.Headers);
        
        await _next(context);
        
        watch.Stop();
        Log.Information("Finished HTTP {RequestMethod} {RequestPath} request at {FinishDateTime:dd.MM.yyyy HH:mm:ssZ}, elapsed time: {ElapsedMilliseconds} ms\nWith status code: {ResponseStatusCode} {Reason}\nHeaders: {ResponseHeaders}",
            context.Request.Method, context.Request.Path, DateTime.UtcNow, watch.ElapsedMilliseconds,
            context.Response.StatusCode, (HttpStatusCode) context.Response.StatusCode, context.Response.Headers);
    }
}