using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (string title, string details ,int statusCode) = exception switch
        {
            NotFoundException => ("Not Found", exception.Message, StatusCodes.Status404NotFound),
            ValidationException => ("Validation Error", exception.Message, StatusCodes.Status400BadRequest),
            _ => ("Internal Server Error", exception.Message, StatusCodes.Status500InternalServerError)
        };
        ProblemDetails problemDetails = new ()
        {
            Title = title,
            Detail = details,
            Status = statusCode
        };
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
