using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{

    public class OrderController : Controller
    {
        /// <summary>
        /// This class generates a default view and passes a new Order object to the view
        /// </summary>
        /// <returns></returns>
        public ViewResult Checkout() => View(new Order());
    }
}
