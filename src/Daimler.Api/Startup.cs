using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Daimler.Api.Application.Mappers;
using Daimler.Api.Infrastructure;
using Daimler.Lib.Middlewares;
using Daimler.Lib.Modules;

namespace Daimler.Api
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
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:6109", "https://localhost:8081", "http://localhost:61091", "https://localhost:61092")
                        .AllowCredentials();
                }));

            services
                    .AddMvc()
                    .AddControllersAsServices()
                    .AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });
         
            services.AddOptions();
            services.AddSwaggerGen();
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Daimler.Api V1");
            });

            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseHealthChecks("/hc");
            app.UseStaticFiles();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new AutoMapperModule());
            builder.RegisterModule(new DataAccessModule());
            builder.RegisterModules(_configuration, typeof(Startup).Assembly);
        }
    }
}
