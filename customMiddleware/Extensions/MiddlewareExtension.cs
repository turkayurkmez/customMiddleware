using customMiddleware.Infrastructure;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customMiddleware.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UsePostHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PostHandlerMiddleware>();
        }

        public static IApplicationBuilder UseCalculateResponseTime(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CalculateResponseTimeMiddleware>();
        }
    }
}
