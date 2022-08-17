using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AdapterImec.Api.Middleware
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogDebug($"Request started {DumpRequest(context.Request)}");

            try
            {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                await next(context);
                sw.Stop();

                var message = $"Request finished {DumpRequest(context.Request)} (in {sw.ElapsedMilliseconds} ms) ({DumpResponse(context.Response)})";
                if (IsHealthPath(context))
                {
                    _logger.LogDebug(message);
                }
                else
                {
                    _logger.LogInformation(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failed {DumpRequest(context.Request)} ({ex.GetType().Name}: {ex.Message})");
                throw;
            }
        }

        private static bool IsHealthPath(HttpContext context)
        {
            var healthPath = new PathString("/health");
            return context.Request.Path.StartsWithSegments(healthPath);
        }

        private string DumpRequest(HttpRequest request)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(request.Method);
            sb.Append(": ");
            sb.Append(request.Path);

            if (request.QueryString.HasValue)
            {
                sb.Append(" ");
                sb.Append(request.QueryString.Value);
            }

            return sb.ToString();
        }

        private string DumpResponse(HttpResponse response)
        {
            if (response is null)
            {
                return null;
            }

            var statusCode = (HttpStatusCode)response.StatusCode;

            return $"{response.StatusCode} - {statusCode}";
        }
    }
}
