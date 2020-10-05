using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ClassDemoRestPizza
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

            services.AddCors(
                option =>
                {
                    option.AddPolicy("PetersPizza",
                        builder => builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "PUT"));
                }
            );

            services.AddSwaggerGen(  
                c=> c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title="Pizza", Version="v1.0",
                    Contact = new OpenApiContact()
                    {
                        Email = "pele@easj.dk",
                        Name="Peter L", 
                        Url=new Uri("http://pele-easj.dk")
                    },
                    Description = "Pele API",

                })
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("PetersPizza");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API 1.0");
                    c.RoutePrefix = "api/help";

                }
            );


        }
    }
}
