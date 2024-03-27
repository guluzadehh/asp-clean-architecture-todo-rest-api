using System.Text.Json;
using Todo.Application.Exceptions;
using ApplicationException = Todo.Application.Exceptions.ApplicationException;

namespace Todo.WebApi.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            if (e is not Todo.Application.Exceptions.ApplicationException)
            {
                _logger.LogError(e, e.Message);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetStatusCode(e);
            await context.Response.WriteAsync(JsonSerializer.Serialize(
                new
                {
                    message = GetMessage(e),
                    errors = GetErrors(e)
                }
            ));
        }
    }

    private int GetStatusCode(Exception exception) =>
        exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

    private string GetMessage(Exception exception) =>
        (exception is ApplicationException) ? exception.Message : "Internal Error";

    private IReadOnlyDictionary<string, string>? GetErrors(Exception exception) =>
        (exception is ApplicationException appExc) ? appExc.Errors : null;
}
