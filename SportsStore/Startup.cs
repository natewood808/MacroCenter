using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SportsStore
{
    public class Startup
    {
        // This property and constructor loads the configuration settings in appsettings.json and 
        // makes it available through the Configuration property.
        IConfigurationRoot Configuration;
        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IProductRepository, FakeProductRepository>(); // This was used for test data before we had a database

            // This line sets up the services provided by EF Core for the database context class.
            // We also configure the database with the UseSqlServer method and specified a connection string,
            // this connection string is located in the Configuration property we made in the constructor.
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            // Whenever a component (i.e. ProductController) needs an instance of IProductRepository, MVC provides
            // the class EFProductRepository which implements the interface.
            services.AddTransient<IProductRepository, EFProductRepository>();

            // Sets up "shared objects" used in MVC applications
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                // Place routes over the default one since the routing system considers routes
                // in the order they are listed. So top routes take precedence over lower ones.

                // The routes below follow the given formats:
                // Product/List/Soccer/Page2
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{page:int}",
                    defaults: new { controller = "Product", action = "List" }
                );
                // Product/List/Page2
                routes.MapRoute(
                    name: null,
                    template: "Page{page:int}",
                    defaults: new { controller = "Product", action = "List" }
                );
                // Product/List/Soccer
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Product", action = "List" }
                );
                // Product/List/
                routes.MapRoute(
                    name: null, 
                    template: "",
                    defaults: new { controller = "Product", action = "List" }
                );
                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}"
                );
            });
        }
    }
}
