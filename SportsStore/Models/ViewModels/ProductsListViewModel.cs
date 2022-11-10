using System.Collections.Generic;

namespace SportsStore.Models.ViewModels
{
    // This class is used to wrap all the data we are going to send
    // from the controller to the view.

    // We update the List action method in ProductController.cs
    // to use this class to provide the view with details of the
    // products to display on the page and details of the pagination.

    // Updated to include the current category being filtered by, if any.
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
