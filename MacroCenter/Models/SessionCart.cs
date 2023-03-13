using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using MacroCenter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacroCenter.Models
{
    /// <summary>
    /// This class subclasses the Cart class and overrides the methods AddItem, RemoveLine,
    /// and Clear. This is a class that is aware of how to store itself in the session state.
    /// </summary>
    public class SessionCart : Cart
    {
        /// <summary>
        /// Factory that creates SessionCart objects and provides these objects
        /// with an ISession object so they can store themselves. Getting this ISession
        /// object is done through gettting an IHttpContextAcessor service and from there
        /// an HttpContext object which has the ISession object. This is because
        /// the session is provided as a regular service.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        /// <summary>
        /// Calls the base implementation (Cart.AddItem) and stores the
        /// updated state of the Cart in the session using the 
        /// extension methods on the ISession interface, these methods
        /// are defined in SessionExtensions.cs.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
