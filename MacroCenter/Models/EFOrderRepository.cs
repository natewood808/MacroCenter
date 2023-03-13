using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacroCenter.Models
{
    /// <summary>
    /// I'm not 100% sure how this class works. Getting there though.
    ///
    /// </summary>
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        // This is an expression body definition for the Orders property. Basically
        // a get accessor which retrieves the Product info from the CartLine objects
        // in all the Order objects in the database context.
        public IEnumerable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product); 

        /// <summary>
        /// This is where the saving of Order objects happens.
        /// </summary>
        /// <param name="order"></param>
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
