using WebApi.Middlewares;

namespace WebApi.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
