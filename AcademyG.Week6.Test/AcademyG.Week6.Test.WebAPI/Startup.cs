using AcademyG.Week6.Test.Core.BL;
using AcademyG.Week6.Test.Core.EF;
using AcademyG.Week6.Test.Core.EF.Repositories;
using AcademyG.Week6.Test.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AcademyG.Week6.Test.WebAPI
{
    public class Startup
    {
        public readonly string ApplicationName = Assembly.GetEntryAssembly().GetName().Name;

        public readonly string ApplicationVersion = $"v{Assembly.GetEntryAssembly().GetName().Version.Major}" +
                                                    $".{Assembly.GetEntryAssembly().GetName().Version.Minor}" +
                                                    $".{Assembly.GetEntryAssembly().GetName().Version.Build}";


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.MaxDepth = 2;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });



            services.AddTransient<IMainBusinessLayer, MainBusinessLayer>();
            services.AddTransient<IClientRepository, EFClientRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddDbContext<ManagementContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TestWeek6"));
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = ApplicationName,
                    Version = ApplicationVersion
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", $"{ApplicationName} {ApplicationVersion}");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
