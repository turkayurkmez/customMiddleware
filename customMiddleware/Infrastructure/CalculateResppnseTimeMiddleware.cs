using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customMiddleware.Infrastructure
{
    public class CalculateResponseTimeMiddleware
    {
        private RequestDelegate next;
        private DurationResponse durationResponse;

        public CalculateResponseTimeMiddleware(RequestDelegate next, DurationResponse durationResponse)
        {
            this.next = next;
            this.durationResponse = durationResponse;
         
        }

        public async Task Invoke(HttpContext context)
        {

            durationResponse.Start();

            if (context.Request.Method.Equals("GET"))
            {
                var result = durationResponse.Duration;
                // await context.Response.WriteAsync($"bu get işlemi {result} milisaniyr sürdü");
                await context.Response.WriteAsJsonAsync<string>($"{result} milisaniye....");
                return;
            }
            await next.Invoke(context);
        }
    }
}
