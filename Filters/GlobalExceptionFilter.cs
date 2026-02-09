using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace capstone_policy_management.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var errorCode = "INTERNAL_SERVER_ERROR";
        var message = "An unexpected error occurred";

        // Handle specific exception types
        if (context.Exception is UnauthorizedAccessException)
        {
            statusCode = HttpStatusCode.Unauthorized;
            errorCode = "UNAUTHORIZED";
            message = context.Exception.Message;
        }
        else if (context.Exception is InvalidOperationException)
        {
            statusCode = HttpStatusCode.BadRequest;
            errorCode = "INVALID_OPERATION";
            message = context.Exception.Message;
        }
        else if (context.Exception is ArgumentException)
        {
            statusCode = HttpStatusCode.BadRequest;
            errorCode = "INVALID_ARGUMENT";
            message = context.Exception.Message;
        }
        else if (context.Exception is KeyNotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            errorCode = "RESOURCE_NOT_FOUND";
            message = context.Exception.Message;
        }

        // Log the exception
        _logger.LogError(context.Exception, 
            "Exception occurred: {ErrorCode} - {Message}", 
            errorCode,
            context.Exception.Message);

        // Create standardized error response matching requirements
        var errorResponse = new
        {
            errorCode = errorCode,
            message = message,
            traceId = context.HttpContext.TraceIdentifier
        };

        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true;
    }
}
