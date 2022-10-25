using System.Net;
using System.Text.Json;
using Postter.Common.Exceptions;

namespace FINSTAR_Test_Task.Common.Middlewares;

/// <summary>
/// Обработчик ошибок
/// </summary>
public sealed class ExceptionMiddleware
{
    public ExceptionMiddleware(RequestDelegate nextRequestDelegate, ILogger logger)
    {
        _logger = logger;
        _nextRequestDelegate = nextRequestDelegate;
    }

    private readonly RequestDelegate _nextRequestDelegate;
    private readonly ILogger _logger;
    
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _nextRequestDelegate(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                NullReferenceException x => (int)HttpStatusCode.BadRequest,
                RequestLogicException x => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException x => (int)HttpStatusCode.Unauthorized,
                DirectoryNotFoundException x => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            string result = JsonSerializer.Serialize(response);
            await response.WriteAsync(result);
        }
    }
}