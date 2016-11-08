using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

        public UnhandledExceptionMiddleware(RequestDelegate next, 
            ILogger<UnhandledExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await Handle(context, ex);
            }
        }

        private async Task Handle(HttpContext context, Exception exception)
        {
            if (exception == null) return;

            _logger.LogError("An error occurred: {Message}. Stack Trace: {StackTrace}, Inner Message: {InnerMessage}, Inner StackTrace: {InnerStackTrace}",
                exception.Message, exception.StackTrace, exception.InnerException?.Message, exception.InnerException?.StackTrace);

            //todo: check if production, don't return stack details of exception
            await WriteExceptionAsync(context, exception).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "text/json";
            response.StatusCode = 500;
            await response.WriteAsync(
                JsonConvert.SerializeObject(new {
                    Message = exception.Message,
                    StackTrace = exception.StackTrace
                }, JsonSerializationSettings.Default())
            ).ConfigureAwait(false);
        }
    }
}