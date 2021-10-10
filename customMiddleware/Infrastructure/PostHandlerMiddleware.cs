using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customMiddleware.Infrastructure
{
    public class PostHandlerMiddleware
    {
        private RequestDelegate next;

        public PostHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.Request.Method == "POST")
            {
                var isBodyJson = context.Request.HasJsonContentType();
                /*
                 * Bu api projesi, her controller'ında site kullanıcısının yorum girebileceği bir haber portalı tarafından kullanılmaktadır.
                 * 
                 * Kullanıcı, hakaret, küfür gibi hoş olmayan kelimeleri kullanamaz!!!!  
                 */
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;

            }
            await next.Invoke(context);
        }
    }
}
