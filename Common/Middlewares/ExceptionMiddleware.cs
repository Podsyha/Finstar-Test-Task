using System.Net;
using System.Text.Json;
using FINSTAR_Test_Task.Common.Exceptions;

namespace FINSTAR_Test_Task.Common.Middlewares;

/// <summary>
/// Обработчик ошибок
/// </summary>
public sealed class ExceptionMiddleware
{
    public ExceptionMiddleware(RequestDelegate nextRequestDelegate)
    {
        _nextRequestDelegate = nextRequestDelegate;
    }

    private readonly RequestDelegate _nextRequestDelegate;


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
                NullReferenceException x => (int)HttpStatusCode.NotFound,
                RequestLogicException x => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException x => (int)HttpStatusCode.Unauthorized,
                DirectoryNotFoundException x => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.BadRequest
            };

            string result = JsonSerializer.Serialize(error.Message);
            await response.WriteAsync(result);
        }
    }
}