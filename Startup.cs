using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using dotnet_project.Models;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;

namespace dotnet_project
{
    public class Startup
    {
        private IHostingEnvironment CurrentEnvironment { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            CurrentEnvironment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure Database
            if (CurrentEnvironment.IsProduction())
            {
                DbConnectionStringBuilder builder = new DbConnectionStringBuilder();

                var parsedUrl = new Uri(Environment.GetEnvironmentVariable("DATABASE_URL"));
                var user = parsedUrl.UserInfo.Split(":")[0];
                var password = parsedUrl.UserInfo.Split(":")[1];
                var host = parsedUrl.Host;
                var port = parsedUrl.Port;
                var database = parsedUrl.Segments[1];
                bool pooling = true;
                
                builder["User ID"] = user;
                builder["Password"] = password;
                builder["Host"] = host;
                builder["Port"] = port;
                builder["Database"] = database;
                builder["Pooling"] = pooling;

                var connectionString = builder.ConnectionString;

                Console.WriteLine(connectionString);
                services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            }
            else {
                services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("ApplicationDb"));
            }

            //Configure MVC
            services.AddMvc();

            //Configuring Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Fitness API", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "dotnet-project.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //Adding Swagger and SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });
        }
    }
}
