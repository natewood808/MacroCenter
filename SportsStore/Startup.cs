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
using Microsoft.AspNetCore.Identity;

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

            // This method specifies that the same object should be used to satisfy related requests for Cart instances.
            // So any Cart required by components handling the same HTTP request will receive the same object.
            // This lambda expression is invoked to satisfy Cart requests. We pass the collection of services
            // that have been registered to the GetCart method, the result is that requests for the Cart service
            // will be handled by creating SessionCart objects, which will serialize themselves as session data
            // when they are modified.
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

            // This method specifies the same object should always be used. The service we created tells MVC
            // to use HttpContextAccessor whenever implementations of the IHttpContextAccessor interface
            // is required. This service is required so we can access the current session in the SessionCart class.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Tells MVC whenever an implementation of the IOrderRepository class is needed
            // use an instance of the EFOrderRepository.
            services.AddTransient<IOrderRepository, EFOrderRepository>();

            // This method sets up Identity services using built-in classes to represent users and roles.
            services.AddIdentity<AppUser, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            // Sets up "shared objects" used in MVC applications
            services.AddMvc(options => options.EnableEndpointRouting = false).AddNewtonsoftJson();
            services.AddMemoryCache(); // Sets up the in-memory data store where we store session data
            services.AddSession(); // Registers the services used to access session data
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication(); // Sets up the components to intercept requests and responses to implement the security policy
            app.UseSession(); // Allows the session system to automatically associate requests with sessions when they arrive from the client
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
