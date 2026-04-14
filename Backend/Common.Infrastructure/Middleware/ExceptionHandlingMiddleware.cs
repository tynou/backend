using System.Net;
using System.Text.Json;
using Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            UnauthorizedException => (HttpStatusCode.Unauthorized, "Unauthorized"),
            ConflictException => (HttpStatusCode.Conflict, "Conflict"),
            _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        
        var response = new 
        {
            Status = (int)statusCode,
            Message = message,
            Details = exception.Message,
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}