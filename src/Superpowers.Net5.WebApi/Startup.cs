using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using Superpowers.Net5.Ef;
using MediatR;
using System.Reflection;
using Microsoft.AspNetCore.ResponseCompression;
using Superpowers.Net5.WebApi.Hubs;
using Todo;

namespace Superpowers.Net5.WebApi
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
            services.AddLogging(b => b.AddConsole());
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Superpowers.Net5.WebApi", Version = "v1" });
            });

            services.AddTodoEntityFramework(Configuration.GetConnectionString("TodoDb"));
            services.AddMediatR(Assembly.GetExecutingAssembly());


            services.AddSignalR();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddGrpcClient<TodoService.TodoServiceClient>(o =>
            {
                o.Address = new Uri("https://localhost:1265");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();             
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Superpowers.Net5.WebApi v1"));

            app.UseHttpsRedirection();

           
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TodoHub>("/todohub");
                endpoints.MapFallbackToFile("index.html");
            });

            app.UseBlazorFrameworkFiles();

        }
    }
}
