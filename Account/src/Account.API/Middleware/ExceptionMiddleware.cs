using Account.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Account.API.Middleware;

public class ExceptionMiddleware
{
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
        context.Response.ContentType = "application/json";

        var statusCode = (int)HttpStatusCode.InternalServerError;
        var result = new
        {
            Message = "Error en el servidor, vuelva a intentar"
        };

        if (exception is AccountNotFoundException notFoundException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            result = new
            {
                notFoundException.Message
            };
        }

        if (exception is MovementNotFoundException movementNotFoundException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            result = new
            {
                movementNotFoundException.Message
            };
        }

        if (exception is NoFundsAvailable noFundsAvailable)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            result = new
            {
                noFundsAvailable.Message
            };
        }

        if (exception is DbUpdateException dbUpdateException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            result = new
            {
                dbUpdateException.Message
            };
        }

        if (exception is Exception e)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            result = new
            {
                e.Message
            };
        }

        context.Response.StatusCode = statusCode;

        // Serialize the response
        return context.Response.WriteAsync(JsonSerializer.Serialize(result));
    }
}
