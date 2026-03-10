using System.Net;
using System.Text.Json;
using FluentValidation;

namespace DocumentRetrievalService.Api.Middleware;

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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new { errors = validationException.Errors.Select(e => e.ErrorMessage) });
                break;
        }

        context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)code;

        if (string.IsNullOrEmpty(result))
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}
