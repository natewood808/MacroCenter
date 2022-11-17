using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    /// <summary>
    /// Initially this class was concerned with how Cart objects were created and persisted.
    /// This was done by having methods that read and wrote data from the session, GetCart()
    /// and SaveCart(). Since implementing the Cart service these methods are obsolete and
    /// focuses the CartController.cs class on its role in the application.
    /// </summary>
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                // Cart = GetCart(), // Get the Cart object from the session state
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                // Cart cart = GetCart();
                cart.AddItem(product, 1);
                // SaveCart(cart);
            }
            // This tells the browser to request a URL that will call the Index action method on this controller.
            return RedirectToAction("Index", new { returnUrl }); 
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                // Cart cart = GetCart();
                cart.RemoveLine(product);
                // SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //private Cart GetCart()
        //{
        //    Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        //    return cart;
        //}

        //private void SaveCart(Cart cart)
        //{
        //    // The HttpContext property is provided the Controller base class,
        //    // and returns an HttpContext object that provides context data about
        //    // the request that has been received and the response that is being prepared.
        //    // The Session property returns an object that implements the ISession interface,
        //    // which is the type we defined our extension method, SetJson(this ISession session ...).

        //    HttpContext.Session.SetJson("Cart", cart);
        //}
    }
}
