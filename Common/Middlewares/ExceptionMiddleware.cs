using System.Net;
using System.Text.Json;
using FINSTAR_Test_Task.Common.Exceptions;

namespace FINSTAR_Test_Task.Common.Middlewares;

/// <summary>
/// Обработчик ошибок
/// </summary>
public sealed class ExceptionMiddleware
{
    public ExceptionMiddleware(RequestDelegate nextRequestDelegate, ILogger<ExceptionMiddleware> logger)
    {
        _nextRequestDelegate = nextRequestDelegate;
        _logger = logger;
    }

    private readonly RequestDelegate _nextRequestDelegate;
    private readonly ILogger<ExceptionMiddleware> _logger;


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
                NullReferenceException => (int)HttpStatusCode.NotFound,
                RequestLogicException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                DirectoryNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.BadRequest
            };

            _logger.LogError(error.Message);

            string result = JsonSerializer.Serialize(error.Message);
            await response.WriteAsync(result);
        }
    }
    
    private static string FormatHeaders(IHeaderDictionary headers) => string.Join(", ", headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value)}}}"));

    private static async Task<string> ReadBodyFromRequest(HttpRequest request)
    {
        // Ensure the request's body can be read multiple times (for the next middlewares in the pipeline).
        request.EnableBuffering();

        using var streamReader = new StreamReader(request.Body, leaveOpen: true);
        var requestBody = await streamReader.ReadToEndAsync();

        // Reset the request's body stream position for next middleware in the pipeline.
        request.Body.Position = 0;
        return requestBody;
    }
}