using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
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
    }
}
