using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Daimler.Lib.Middlewares;
using Daimler.Lib.Modules;
using Serilog;

namespace Daimler.ApiGateway
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _environment; 

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _environment = env; 
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy("AllowMyOrigin", p =>
                {
                    p.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins(
                            "http://localhost:6109",
                            "https://localhost:8081")
                        .AllowCredentials();
                });
            });

            services.AddOcelot();
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            loggerFactory.AddSerilog();
            app.UseRouting();
            app.UseCors("AllowMyOrigin");
            app.UseHealthChecks("/hc");
            app.UseStaticFiles();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseWebSockets();
            app.UseOcelot().Wait();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new SerilogModule(_configuration));
        }
    }
}
