using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacroCenter.Models.ViewModels
{
    /// <summary>
    /// This class will be used to pass two pieces of information to a view:
    /// 1) The Cart object and 2) the URL to display if the user clicks the
    /// Continue Shopping button. This is ultimately used to implement
    /// our Index action method on the CartController.cs class.
    /// </summary>
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
