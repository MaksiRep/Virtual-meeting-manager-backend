using RU.NSU.FIT.VirtualManager.Domain.Exceptions;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedException e)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(Error(e));
        }
        catch (ForbiddenException e)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(Error(e));
        }
        catch (EntityNotFoundException e)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(Error(e));
        }
        catch (BadRequestException e)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(Error(e));
        }   
        catch (Exception e)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(ErrorExtended(e));
        }
    }

    private static object Error(Exception e)
    {
        return new
        {
            Message = e.Message
        };
    }
    
    private static object ErrorExtended(Exception e)
    {
        return new
        {
            Message = e.Message,
            StackTrace = e.StackTrace
        };
    }
}