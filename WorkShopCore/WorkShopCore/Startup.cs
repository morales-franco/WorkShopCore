using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorkShopCore.Business;
using Microsoft.EntityFrameworkCore;

namespace WorkShopCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //register the ArticleBusinessService on the DI container
            services.AddTransient<IArticlesService, ArticlesService>();

            services.AddMvc();

            /*
             * Configuramos EF Core SQLite
             * Paquetes Nuget que se incluyen
             *  Microsoft.EntityFrameworkCore
             *  Microsoft.EntityFrameworkCore.Sqlite
             *  Microsoft.EntityFrameworkCore.Design
             *  Microsoft.EntityFrameworkCore.Tools
             *  
             *  Luego ir al csProject y agregar a mano (esto es para poder hacer migrations)
             *    <ItemGroup>
                    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.3" />
                  </ItemGroup>
             */
            services.AddDbContext<Data.ArticlesContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("Articles"));
            });

        }

        /// <summary>
        /// The method Configure gets called by the runtime, and is required. 
        /// Use this method to configure the HTTP request pipeline. 
        /// IApplicationBuilder is required; provides the mechanisms to configure an application’s request pipeline. 
        /// Middlewares are configured here.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Startup logging
            //Según la configuración del appSettings dev o prod loggeara el nivel correspondiente
            var startupLogger = loggerFactory.CreateLogger<Startup>();
            startupLogger.LogTrace("Trace test output!");
            startupLogger.LogDebug("Debug test output!");
            startupLogger.LogInformation("Info test output!");
            startupLogger.LogError("Error test output!");
            startupLogger.LogCritical("Trace test output!");

            /*
             * Set the hosting environment to "Development" in our OS's environment variables
             * https://andrewlock.net/how-to-set-the-hosting-environment-in-asp-net-core/
             * For Windows, run this in Powershell on the project folder,
             * $Env:ASPNETCORE_ENVIRONMENT = "Development"
             * Default is Production
             */
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Enable WebServer to serve css files, and other's files in wwwroot folder
            //This middleware will enable the feature to serve statics files like css, images, etc
            app.UseStaticFiles();

            //add the MVC middleware
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }

    }
}
