using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace SportsStore.Models
{
    /// <summary>
    /// AFter the initial set up of EF, adding models to the database
    /// is as easy as adding new properties, we added Orders property
    /// and created a new migration which will allow Orders to be stored
    /// in the databse.
    /// 
    /// This is done by running the Add-Migration "Name of Migration" ("Orders" in our case),
    /// in the Tools > NuGet Package Manager > Package Manager Console. This will take a 
    /// snapshot of the application and see how it differs from the current database version
    /// and generate a migration called Orders. We update the schema by running the 
    /// Update-Database command.
    /// 
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}