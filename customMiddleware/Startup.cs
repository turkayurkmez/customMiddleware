using customMiddleware.Infrastructure;
using customMiddleware.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customMiddleware
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddTransient<DurationResponse>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "customMiddleware", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Mudahale ettik. Pipeline durdu");
            //});
           // app.UseWelcomePage();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "customMiddleware v1"));
            }

            app.Map("/apiTest", appBuilder =>
             {
                 appBuilder.Run(async (context) =>
                 {
                     if (context.Request.Query.ContainsKey("id"))
                     {
                         var id = context.Request.Query["id"];
                         await context.Response.WriteAsync($"{id} degeri QueryString araciligi ile gonderildi. Test edildi");
                     }
                     else
                     {
                         await context.Response.WriteAsync("id degeri olmadığından test yapılamadı");
                     }
                 });
             });


         

            // RequestDelegate
         
            app.UseHttpsRedirection();

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Method == "POST")
            //    {
            //        var isBodyJson = context.Request.HasJsonContentType();
            //        /*
            //         * Bu api projesi, her controller'ında site kullanıcısının yorum girebileceği bir haber portalı tarafından kullanılmaktadır.
            //         * 
            //         * Kullanıcı, hakaret, küfür gibi hoş olmayan kelimeleri kullanamaz!!!!  
            //         */
            //        context.Response.StatusCode = StatusCodes.Status400BadRequest;
            //        return;

            //    }
            //    await next.Invoke();
            //});

            // app.UseMiddleware<PostHandlerMiddleware>();
            app.UsePostHandler();
            app.UseCalculateResponseTime();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
