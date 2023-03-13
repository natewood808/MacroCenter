using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MacroCenter.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Product> Products => context.Products;

        /// <summary>
        /// This method is added to IProductRepository and subsequently to
        /// EFProductRepository so that we can save any changes an admin
        /// may post to the product catalog in the database.
        /// </summary>
        /// <param name="product"></param>
        public void SaveProduct(Product product)
        {
            // Add the product to the database if it doesn't exist in the database
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else // Product exists in the database already so find the entry and update it's values.
            {
                Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}