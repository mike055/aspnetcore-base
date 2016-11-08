using Microsoft.AspNetCore.Builder;

namespace Base.Web.Middleware.Exceptions
{
    public static class UnhandledExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseUnhandledExceptionCatching(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UnhandledExceptionMiddleware>();
        }
    }
}