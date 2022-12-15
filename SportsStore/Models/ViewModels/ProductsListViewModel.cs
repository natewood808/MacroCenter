﻿using System.Collections.Generic;

namespace SportsStore.Models.ViewModels
{
    /// This class is used to wrap all the data we are going to send
    /// from the controller to the view.
    
    /// We update the List action method in ProductController.cs
    /// to use this class to provide the view with details of the
    /// products to display on the page and details of the pagination.
    
    /// Updated to include the current category being filtered by, if any.
     
    /// Careful not to extend view models with copious amounts of data as
    /// every action method that uses the view model will need to be updated
    /// and the purpose of action methods may become unclear.
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentPriceSort { get; set; }
    }
}
