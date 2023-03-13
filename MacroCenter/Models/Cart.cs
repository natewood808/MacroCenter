using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacroCenter.Models
{
    public class Cart
    {
        // Our collection of items in the cart
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual IEnumerable<CartLine> Lines => lineCollection; // Property that gets access to all lines in the cart

        public virtual void AddItem(Product product, int quantity)
        {
            // Search the cart to see if the item we are trying to add already exists in the cart
            CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();

            // If the cart doesn't have this item yet do this.
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else // The cart has this item already so increment the quantity specified
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) => lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);

        public virtual decimal ComputeTotalValue() => lineCollection.Sum(e => e.Product.Price * e.Quantity);

        public virtual void Clear() => lineCollection.Clear();
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
