using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestfulApis_Application.Utilities;
using System.Text.Json;

namespace Shared
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception exception)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var errors = new List<Error>() {
                    new Error() { Name="",Message="An unexpected error occurred."}
                };
                string logError = $"""
                                 Error Message: {exception.Message}
                                 TraceId: {context.TraceIdentifier}
                                 Inner Exception Message: {exception.InnerException?.Message}
                                 """;
                _logger.LogError(logError);

                var jsonResponse = JsonSerializer.Serialize(new ErrorResult(500, errors));
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
