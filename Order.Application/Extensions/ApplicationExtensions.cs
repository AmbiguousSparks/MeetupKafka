using Microsoft.AspNetCore.Builder;
using Order.Application.Middlewares;

namespace Order.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UseApplication(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<LogMiddleware>();
            return builder;
        }
    }
}
