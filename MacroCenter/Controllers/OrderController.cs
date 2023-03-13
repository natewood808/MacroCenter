﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MacroCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacroCenter.Controllers
{

    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        /// <summary>
        /// This action method returns the List.cshtml view  and
        /// passes orders that have not been marked as shipped to the view
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ViewResult List() => View(repository.Orders.Where(o => !o.Shipped));

        /// <summary>
        /// This action method finds the Order in the database that matches the 
        /// current orderID and marks that order as shipped in the database.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = repository.Orders.FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }

        /// <summary>
        /// This method returns the Checkout.cshtml view and passes an Order object into the view.
        /// </summary>
        /// <returns></returns>
        public ViewResult Checkout() => View(new Order());

        /// <summary>
        /// This method will be invoked during an HTTP POST request (when the user
        /// submits the form).
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            // Check the ModelState, a property of the Model Binder system. This system also checks
            // the validation constraints we added to the Order.cs class, so if there are any errors,
            // like shipping info not filled out, we will know by checking ModelState.IsValid.
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray(); // Fill the order.Lines property with data from the cart.Lines property
                repository.SaveOrder(order); // Save this order to the repository
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}