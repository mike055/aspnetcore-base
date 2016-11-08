using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Base.Web.Serialization.Json;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Base.Web.Middleware.Exceptions
{
    public class UnhandledExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UnhandledExceptionMiddleware> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UnhandledExceptionMiddleware(RequestDelegate next, 
            ILogger<UnhandledExceptionMiddleware> logger,
            IHostingEnvironment hostingEnvironment)
        {
            _next = next;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleAsync(context, ex);
            }
        }

        private async Task HandleAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;

            _logger.LogError("An error occurred: {Message}. Stack Trace: {StackTrace}, Inner Message: {InnerMessage}, Inner StackTrace: {InnerStackTrace}",
                exception.Message, exception.StackTrace, exception.InnerException?.Message, exception.InnerException?.StackTrace);

            if (_hostingEnvironment.IsDevelopment())
            {
                await WriteExceptionAsync(context, new {
                    Message = exception.Message,
                    StackTrace = exception.StackTrace
                }).ConfigureAwait(false);
            }
            else 
            {
                await WriteExceptionAsync(context, new {
                    Message = "An error has occured"
                }).ConfigureAwait(false);
            }
        }

        private static async Task WriteExceptionAsync(HttpContext context, object responseContent)
        {
            var response = context.Response;
            response.ContentType = "text/json";
            response.StatusCode = 500;
            await response.WriteAsync(
                JsonConvert.SerializeObject(responseContent, JsonSerializationSettings.Default())
            ).ConfigureAwait(false);
        }
    }
}