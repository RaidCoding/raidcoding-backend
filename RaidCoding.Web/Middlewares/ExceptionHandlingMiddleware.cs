using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using RaidCoding.Logic.Exceptions;

namespace RaidCoding.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await HttpExceptionHandlingUtilities.WriteExceptionToContextAsync(context, ex);
        }
    }
}

public static class HttpExceptionHandlingUtilities
{
    public static Task WriteExceptionToContextAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case ValidationException vEx:
            {
                context.Response.StatusCode = (int) ResolveHttpStatusCode(vEx);
                var validationProblemDetails = new ValidationProblemDetails(vEx.Errors)
                {
                    Type = $"{vEx.EntityName}.{vEx.Error}",
                    Title = vEx.Title,
                    Status = (int) ResolveHttpStatusCode(vEx),
                    Detail = exception.Message
                };

                var json = JsonSerializer.Serialize(validationProblemDetails);
                return context.Response.WriteAsync(json);
            }
            case LogicalException lEx:
            {
                context.Response.StatusCode = (int) ResolveHttpStatusCode(lEx);
                var problemDetails = new ProblemDetails
                {
                    Status = (int) ResolveHttpStatusCode(lEx),
                    Type = $"{lEx.EntityName}.{lEx.Error}",
                    Title = lEx.Title,
                    Detail = exception.Message,
                };

                var json = JsonSerializer.Serialize(problemDetails);
                return context.Response.WriteAsync(json);
            }
            case AuthenticationException:
                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                // New schemes can be added in future, but for now it is JWT only.
                context.Response.Headers.Append("WWW-Authenticate", JwtBearerDefaults.AuthenticationScheme);
                break;
            default:
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = (int) HttpStatusCode.InternalServerError,
                    Type = "InternalServerError",
                    Title = "Internal server error.",
                    Detail = "A critical internal server error occurred."
                };

                var json = JsonSerializer.Serialize(problem);
                return context.Response.WriteAsync(json);
            }
        }

        return Task.CompletedTask;
    }
    
    private static HttpStatusCode ResolveHttpStatusCode(LogicalException exception)
    {
        return exception.Error switch
        {
            "AlreadyExists" => HttpStatusCode.Conflict,
            "NotFound" => HttpStatusCode.NotFound,
            "Validation" => HttpStatusCode.BadRequest,
            "WrongFlow" => HttpStatusCode.BadRequest,
            "Restricted" => HttpStatusCode.Forbidden,
            _ => HttpStatusCode.NotImplemented,
        };
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static void UseRcExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}