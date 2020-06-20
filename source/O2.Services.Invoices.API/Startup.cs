using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace O2.Services.Invoices.API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.MapWhen(
            context => context.Request.Headers.ContainsKey("ping"),
            builder =>
            {
                // builder.UseMiddleware<RequestTimingAdHocMiddleware>();
                builder.Run(async (context) => { await context.Response.WriteAsync("pong from header"); });
            });
            app.Map("/ping", builder =>
            {
                // builder.UseMiddleware<RequestTimingFactoryMiddleware>();
                builder.Run(async (context) =>
                {
                    await context.Response.WriteAsync("pong from path");
                });
            });
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting((() =>
                {
                    context.Response.Headers.Add("X.Powered-By", "Asp .Net Core: O2 Invoices");
                    return Task.CompletedTask;
                }));
                await next.Invoke();
            });
            app.UseMvc();
            app.Run(async (context) =>
           {
               await context.Response.WriteAsync("No middlewares could handle the request");
           });
        }
    }
}
