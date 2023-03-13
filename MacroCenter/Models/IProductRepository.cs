using System.Collections;
using System.Collections.Generic;

namespace MacroCenter.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int producttID);
    }
}
