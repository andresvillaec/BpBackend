using Client.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Client.API.Middleware;

public class ExceptionMiddleware
{
    private const string DatabaseError = "Error en la base de datos, vuelva a intentar";
    private const string ServerError = "Error en el servidor, vuelva a intentar";
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = exception switch
        {
            ClientNotFoundException notFoundException => CreateErrorDetails(HttpStatusCode.BadRequest, notFoundException.Message),
            DbUpdateException dbUpdateException => CreateErrorDetails(HttpStatusCode.BadRequest, DatabaseError),
            Exception baseException => CreateErrorDetails(HttpStatusCode.BadRequest, baseException.Message),
            _ => CreateErrorDetails(HttpStatusCode.BadRequest, ServerError)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static HttpCustomResponse CreateErrorDetails(HttpStatusCode statusCode, string message)
        => new HttpCustomResponse((int)statusCode, statusCode.ToString(), message);

    private record HttpCustomResponse(
        int statusCode,
        string statusCodeDescription,
        string message);

}
