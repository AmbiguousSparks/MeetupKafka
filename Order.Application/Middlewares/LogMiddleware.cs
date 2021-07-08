using Microsoft.AspNetCore.Http;
using Serilog;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Order.Application.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception e)
            {
                Log.Error($"There was an error executing {_next.Method.Name}.\nMessage: {e.Message}.\nStack Trace: {e.StackTrace}");
                throw new OrderException(e.Message);
            }
        }
    }
}
