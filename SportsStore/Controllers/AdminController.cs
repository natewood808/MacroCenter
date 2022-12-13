using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Calls the View method to select the default view for the action,
        /// passing the set of products in the database as the view's model.
        /// </summary>
        /// <returns></returns>
        public ViewResult Index() => View(repository.Products);

        /// <summary>
        /// Finds the product that corresponds with the productId parameter
        /// (the same product in the same row as the Edit button that was clicked),
        /// and passes it to the Edit.cshtml view.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ViewResult Edit(int productId) => View(repository.Products.FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                // This is a ASP.NET Core seession state feature. It is a key/value dictionary similar to the session
                // data and view bag features we used previously. Temp Data persists until it is read, while session
                // data persists until we explicitly remove it which is something we prefer NOT to do.
                TempData["message"] = $"{product.Name} has been saved!";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        /// <summary>
        /// Provides a new Product object to the Edit.cshtml view so that the
        /// fields are populated with empty values. Makes use of existing Edit
        /// functionality to add new products to the database.
        /// </summary>
        /// <returns></returns>
        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                // An interesting observation of this line is that we can use the same TempData["message"] to display
                // a messsage whenever a product is deleted in the repository or updated successfully. Thanks to
                // the shared layout of that is _AdminLayout.cshtml for implementing the message in TempData.
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
